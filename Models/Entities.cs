using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JLITE.API.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public string Unit { get; set; } = "/pc";
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // mcb, rccb, led, switch, cable, spd, db, etc.
        public string Description { get; set; } = string.Empty;
        public List<string> Specs { get; set; } = new();
        public Dictionary<string, string> DetailedSpecs { get; set; } = new();
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> Gallery { get; set; } = new();
        public string Tag { get; set; } = string.Empty; // Best Seller, New, B2B Special
        public string TagColor { get; set; } = string.Empty;
        public int StockQuantity { get; set; } = 100;
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = true;
        public List<string> Certifications { get; set; } = new() { "ISI Mark", "CE Certified" };
        public decimal Rating { get; set; } = 4.8m;
        public int ReviewCount { get; set; } = 24;
        public int B2BMinQuantity { get; set; } = 10;
        public decimal B2BDiscountPercent { get; set; } = 12.5m;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = "zap";
        public string ImageUrl { get; set; } = string.Empty;
        public int ProductCount { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }

    public class HeroBanner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string HighlightText { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string BadgeText { get; set; } = string.Empty;
        public List<TrustStat> TrustStats { get; set; } = new();
        public string ImageUrl { get; set; } = string.Empty;
        public string CtaPrimary { get; set; } = "Explore Switchgear";
        public string CtaSecondary { get; set; } = "Book 33kV Consultation";
    }

    public class TrustStat
    {
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }

    public class ProjectItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // High Voltage 33kV, Industrial Panel, Substation
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string CompletionYear { get; set; } = "2025";
    }

    public class ClientLogo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }

    public class ContactSubmission
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer"; // Customer, Contractor, Admin
        public string CompanyName { get; set; } = string.Empty;
        public string GSTIN { get; set; } = string.Empty;
        public bool IsB2BVerified { get; set; } = false;
        public List<Address> Addresses { get; set; } = new();
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Address
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Label { get; set; } = "Home";
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = "Tamil Nadu";
        public string Pincode { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false;
    }

    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string OrderNumber { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string GSTIN { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new();
        public Address ShippingAddress { get; set; } = new();
        
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; } // 18% GST calculation
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        
        public string PaymentMethod { get; set; } = "PhonePe"; // PhonePe, COD, NetBanking
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, Failed, Refunded
        public string PhonePeTransactionId { get; set; } = string.Empty;
        public string PhonePeProviderReferenceId { get; set; } = string.Empty;

        public string OrderStatus { get; set; } = "Pending"; // Pending, Processing, Shipped, Delivered, Cancelled
        public string TrackingNumber { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class OrderItem
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class ServiceRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string RequestNumber { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty; // Contracting, Consultancy, LightingDesign, Maintenance
        public string CustomerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string ProjectLocation { get; set; } = string.Empty;
        public string VoltageRating { get; set; } = "Low Voltage (<1kV)"; // Up to 33kV High Voltage
        public string BudgetRange { get; set; } = string.Empty;
        public string ProjectDetails { get; set; } = string.Empty;
        public string Status { get; set; } = "Submitted"; // Submitted, UnderReview, Assigned, InProgress, Completed
        public string AssignedEngineer { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; } = DateTime.UtcNow.AddDays(3);
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class QuoteRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string QuoteNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string GSTIN { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public List<QuoteItem> RequestedItems { get; set; } = new();
        public string Status { get; set; } = "Pending"; // Pending, Quoted, Accepted, Closed
        public decimal? QuotedAmount { get; set; }
        public string AdminNotes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class QuoteItem
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string TargetPrice { get; set; } = string.Empty;
    }

    public class PolicyContent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty; // privacy-policy, terms-and-conditions, refund-cancellation, shipping-delivery
        public string Title { get; set; } = string.Empty;
        public string ContentMarkdown { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
