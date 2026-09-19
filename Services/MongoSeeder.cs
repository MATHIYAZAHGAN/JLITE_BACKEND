using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JLITE.API.Models;
using MongoDB.Driver;

namespace JLITE.API.Services
{
    public class MongoSeeder
    {
        private readonly MongoDbContext _context;
        private readonly IJwtService _jwtService;

        public MongoSeeder(MongoDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task SeedAsync()
        {
            try
            {
                // Clear and re-seed CMS for exact paths & contrast
                await _context.HeroBanners.DeleteManyAsync(Builders<HeroBanner>.Filter.Empty);
                await _context.Projects.DeleteManyAsync(Builders<ProjectItem>.Filter.Empty);
                await _context.Clients.DeleteManyAsync(Builders<ClientLogo>.Filter.Empty);

                // 1. Seed Hero Banner & Trust Stats
                var hero = new HeroBanner
                {
                    Title = "High-Voltage Power & Smart Electrical Solutions",
                    HighlightText = "Tamil Nadu's Trusted Govt. A-Grade License Up to 33kV",
                    Subtitle = "Manufacturer & Direct Supplier of ISI/CE Certified Switchgear, MCBs, RCCBs, DB Boxes, Smart Touch Switches & Armoured Cables.",
                    BadgeText = "ISI & CE Certified • 5-Year Warranty • 24h Express Dispatch",
                    TrustStats = new List<TrustStat>
                    {
                        new TrustStat { Value = "50,000+", Label = "Contractors & B2B Buyers" },
                        new TrustStat { Value = "33kV", Label = "A-Grade Govt. License" },
                        new TrustStat { Value = "40+", Label = "Countries Exported" },
                        new TrustStat { Value = "99.9%", Label = "On-Time Dispatch" }
                    },
                    ImageUrl = "assets/hero-panel.png"
                };
                await _context.HeroBanners.InsertOneAsync(hero);

                // 2. Seed Contracting Projects
                var projects = new List<ProjectItem>
                {
                    new ProjectItem
                    {
                        Title = "33kV Substation Turnkey Contracting",
                        Category = "High Voltage Contracting",
                        Location = "Sriperumbudur Industrial Corridor, TN",
                        Description = "Complete erection, testing, and commissioning of 33kV outdoor switchyard with vacuum circuit breakers.",
                        ImageUrl = "assets/work/Picture34.jpg",
                        CompletionYear = "2025"
                    },
                    new ProjectItem
                    {
                        Title = "Smart Commercial Lighting & Automation",
                        Category = "Architectural Lighting",
                        Location = "IT Park, OMR Chennai",
                        Description = "18W Tunable CCT Smart LED installation integrated with Zigbee touch panels and automatic lux sensors.",
                        ImageUrl = "assets/work/Picture37.jpg",
                        CompletionYear = "2025"
                    },
                    new ProjectItem
                    {
                        Title = "Industrial Power Distribution & Busduct",
                        Category = "Switchgear & Panel Fabrication",
                        Location = "Ambattur Industrial Estate, Chennai",
                        Description = "Main LT distribution panel fabrication with 63A RCCB earth fault protection and SWA armoured cable laying.",
                        ImageUrl = "assets/work/Picture38.jpg",
                        CompletionYear = "2024"
                    }
                };
                await _context.Projects.InsertManyAsync(projects);

                // 3. Seed Client Logos
                var clients = new List<ClientLogo>
                {
                    new ClientLogo { Name = "L&T Construction", LogoUrl = "assets/light pitcher/Picture13.jpg" },
                    new ClientLogo { Name = "TATA Projects", LogoUrl = "assets/light pitcher/Picture18.jpg" },
                    new ClientLogo { Name = "BHEL", LogoUrl = "assets/light pitcher/Picture23.png" },
                    new ClientLogo { Name = "Siemens India", LogoUrl = "assets/light pitcher/Picture24.jpg" },
                    new ClientLogo { Name = "Schneider Electric", LogoUrl = "assets/light pitcher/Picture25.jpg" }
                };
                await _context.Clients.InsertManyAsync(clients);

                // 4. Seed Categories if empty
                if (await _context.Categories.CountDocumentsAsync(Builders<Category>.Filter.Empty) == 0)
                {
                    var categories = new List<Category>
                    {
                        new Category { Name = "Switchgear & MCBs", Slug = "switchgear", Description = "Industrial circuit breakers, RCCBs, DB boxes, and protective equipment.", Icon = "zap", ProductCount = 6 },
                        new Category { Name = "Smart Switches", Slug = "smart-switches", Description = "Wi-Fi & Zigbee touch switches compatible with Alexa & Google Home.", Icon = "wifi", ProductCount = 2 },
                        new Category { Name = "LED & Architectural Lighting", Slug = "lighting", Description = "High-efficacy LED panels, downlights, floodlights, and battens.", Icon = "sun", ProductCount = 3 },
                        new Category { Name = "Cables & Wiring", Slug = "wiring", Description = "Armoured SWA cables, industrial wiring accessories, and conduits.", Icon = "cable", ProductCount = 2 },
                        new Category { Name = "Surge & Power Protection", Slug = "protection", Description = "Type 2 SPD surge protectors and HRC fuses up to 33kV protection.", Icon = "shield-check", ProductCount = 2 }
                    };
                    await _context.Categories.InsertManyAsync(categories);
                }

                // 5. Seed Products if empty
                if (await _context.Products.CountDocumentsAsync(Builders<Product>.Filter.Empty) == 0)
                {
                    var products = new List<Product>
                    {
                        new Product
                        {
                            Name = "JJ MCB 32A Double Pole",
                            Slug = "jj-mcb-32a",
                            SKU = "JL-MCB-32A",
                            Price = 1499,
                            OriginalPrice = 1899,
                            Unit = "/pc",
                            CategoryName = "Switchgear & MCBs",
                            Type = "mcb",
                            Description = "High performance double-pole miniature circuit breaker with 6kA breaking capacity and thermal-magnetic trip unit.",
                            Specs = new List<string> { "32A Current Rating", "6kA Breaking Capacity", "IEC 60898 Certified", "DIN Rail Mount" },
                            ImageUrl = "assets/Picture1.jpg",
                            Tag = "Best Seller",
                            TagColor = "elec",
                            StockQuantity = 150,
                            IsFeatured = true,
                            Rating = 4.9m,
                            ReviewCount = 48
                        },
                        new Product
                        {
                            Name = "Smart LED Panel 18W Tunable CCT",
                            Slug = "smart-led-panel-18w",
                            SKU = "JL-LED-18W",
                            Price = 1999,
                            OriginalPrice = 2499,
                            Unit = "/pc",
                            CategoryName = "LED & Architectural Lighting",
                            Type = "led",
                            Description = "Ultra-slim recessed square LED panel with tunable color temperature (3000K-6500K), Wi-Fi & App control.",
                            Specs = new List<string> { "18W Power Rating", "1800 Lumens", "IP44 Water Resistance", "Wi-Fi + Zigbee" },
                            ImageUrl = "assets/Picture2.jpg",
                            Tag = "New Launch",
                            TagColor = "green",
                            StockQuantity = 80,
                            IsFeatured = true,
                            Rating = 4.8m,
                            ReviewCount = 32
                        },
                        new Product
                        {
                            Name = "JJ Smart Touch Switch 4-Gang",
                            Slug = "jj-smart-switch-4gang",
                            SKU = "JL-SW-4G",
                            Price = 2699,
                            OriginalPrice = 3299,
                            Unit = "/pc",
                            CategoryName = "Smart Switches",
                            Type = "switch",
                            Description = "Tempered glass touch capacitive smart switch with backlight indicator. Voice control enabled via Amazon Alexa & Google Home.",
                            Specs = new List<string> { "10A Per Gang", "2.4GHz Wi-Fi", "Tempered Glass Panel", "Feather Touch" },
                            ImageUrl = "assets/work/Picture3.jpg",
                            Tag = "B2B Choice",
                            TagColor = "amber",
                            StockQuantity = 120,
                            IsFeatured = true,
                            Rating = 4.7m,
                            ReviewCount = 19
                        },
                        new Product
                        {
                            Name = "Distribution Board 8-Way Double Door",
                            Slug = "db-box-8-way",
                            SKU = "JL-DB-8W",
                            Price = 5499,
                            OriginalPrice = 6499,
                            Unit = "/pc",
                            CategoryName = "Switchgear & MCBs",
                            Type = "db",
                            Description = "Powder-coated sheet steel distribution board enclosure, 8-way vertical DB box with transparent acrylic door.",
                            Specs = new List<string> { "8-Way SPN", "IP40 Enclosure", "Zinc Plated DIN Rail", "Reversible Door" },
                            ImageUrl = "assets/work/Picture4.jpg",
                            Tag = "Heavy Duty",
                            TagColor = "slate",
                            StockQuantity = 45,
                            IsFeatured = true,
                            Rating = 4.9m,
                            ReviewCount = 27
                        },
                        new Product
                        {
                            Name = "RCCB 63A 30mA 4-Pole Earth Leakage",
                            Slug = "rccb-63a-30ma",
                            SKU = "JL-RCCB-63A",
                            Price = 3499,
                            OriginalPrice = 4199,
                            Unit = "/pc",
                            CategoryName = "Switchgear & MCBs",
                            Type = "rccb",
                            Description = "Four-pole residual current circuit breaker designed for human shock protection and earth leakage safety.",
                            Specs = new List<string> { "63A Rating", "30mA Sensitivity", "4-Pole 415V", "IEC 61008 Standard" },
                            ImageUrl = "assets/work/Picture5.jpg",
                            Tag = "Safety First",
                            TagColor = "red",
                            StockQuantity = 90,
                            IsFeatured = true,
                            Rating = 5.0m,
                            ReviewCount = 54
                        },
                        new Product
                        {
                            Name = "Armoured Power Cable 4-Core 4mm² SWA",
                            Slug = "armoured-cable-4mm",
                            SKU = "JL-CBL-4MM",
                            Price = 699,
                            OriginalPrice = 799,
                            Unit = "/m",
                            CategoryName = "Cables & Wiring",
                            Type = "cable",
                            Description = "Steel wire armoured (SWA) heavy duty 4-core copper cable for direct burial underground up to 1kV.",
                            Specs = new List<string> { "4 x 4mm² Conductors", "600/1000V Rated", "BS5467 Heavy Duty", "XLPE Insulated" },
                            ImageUrl = "assets/Picture6.jpg",
                            Tag = "Best Seller",
                            TagColor = "elec",
                            StockQuantity = 1000,
                            IsFeatured = true,
                            Rating = 4.9m,
                            ReviewCount = 61
                        }
                    };
                    await _context.Products.InsertManyAsync(products);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Seeder Error]: {ex.Message}");
            }
        }
    }
}
