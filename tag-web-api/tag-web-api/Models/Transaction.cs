// <copyright file="Transaction.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a financial transaction in the system
    /// </summary>
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        public int ArtistID { get; set; }

        public string DeliveryMethod { get; set; }

        public decimal Discount { get; set; }

        public string Note { get; set; }

        public int RefundID { get; set; }

        public string State { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxesWithheld { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal Total { get; set; }

        public string TrackingInfo { get; set; }

        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the User associated with this transaction
        /// </summary>
        public User User { get; set; }
    }
}
