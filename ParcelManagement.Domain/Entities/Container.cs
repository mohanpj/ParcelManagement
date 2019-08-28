using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ParcelManagement.Domain.Entities
{
    [XmlRoot(ElementName = "Address")]
    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }

    [XmlInclude(typeof(Company))]
    [XmlRoot(ElementName = "Sender")]
    public class Sender
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public string CcNumber { get; set; }
    }

    public class Company : Sender { }

    [XmlRoot(ElementName = "Receipient")]
    public class Receipient
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    [XmlRoot(ElementName = "Parcel")]
    public class Parcel
    {
        public Sender Sender { get; set; }
        public Receipient Receipient { get; set; }
        public decimal Weight { get; set; }
        public decimal Value { get; set; }
    }

    [XmlRoot(ElementName = "parcels")]
    public class Parcels
    {
        [XmlElement(ElementName = "Parcel")]
        public List<Parcel> Parcel { get; set; }
    }

    [XmlRoot(ElementName = "Container")]
    public class Container
    {
        public string Id { get; set; }
        public string ShippingDate { get; set; }
        [XmlElement(ElementName = "parcels")]
        public Parcels Parcels { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
    }
}
