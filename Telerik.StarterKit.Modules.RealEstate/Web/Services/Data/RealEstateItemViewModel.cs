using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.StarterKit.Modules.RealEstate.Model;

namespace Telerik.StarterKit.Modules.RealEstate.Web.Services.Data
{
    public class RealEstateItemViewModel : ContentViewModelBase
    {


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RealEstateItemViewModel"/> class.
        /// </summary>
        public RealEstateItemViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RealEstateItemViewModel"/> class.
        /// </summary>
        /// <param name="contentItem">The content item.</param>
        /// <param name="provider">The provider.</param>
        public RealEstateItemViewModel(RealEstateItem contentItem, ContentDataProviderBase provider)
            : base(contentItem, provider)
        {
            this.itemNumber = contentItem.ItemNumber;
            this.address = contentItem.Address;
            this.postalCode = contentItem.PostalCode;
            this.housing = contentItem.Housing;
            this.numberOfRooms = contentItem.NumberOfRooms;
            this.numberOfFloors = contentItem.NumberOfFloors;
            this.yearBuilt = contentItem.YearBuilt;
            this.price = contentItem.Price;
            this.payment = contentItem.Payment;
            this.monthlyRate = contentItem.MonthlyRate;
            this.net = contentItem.Net;
            this.priceSquareMeter = contentItem.PriceSquareMeter;
            this.agentId = contentItem.AgentId;
        }



        #endregion
        
        #region Overrides

        /// <summary>
        /// Get live version of this.ContentItem using this.provider
        /// </summary>
        /// <returns>Live version of this.ContentItem</returns>
        protected override Content GetLive()
        {
            return this.provider.GetLiveBase<RealEstateItem>((RealEstateItem)this.ContentItem);
        }

        /// <summary>
        /// Get temp version of this.ContentItem using this.provider
        /// </summary>
        /// <returns>Temp version of this.ContentItem</returns>
        protected override Content GetTemp()
        {
            return this.provider.GetTempBase<RealEstateItem>((RealEstateItem)this.ContentItem);
        }

        #endregion

        #region Own properties

        /// <summary>
        /// Item number
        /// </summary>
        public string ItemNumber
        {
            get { return this.itemNumber; }
            set { this.itemNumber = value; }
        }

        /// <summary>
        /// Item address
        /// </summary>
        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        /// <summary>
        /// Item address
        /// </summary>
        public string PostalCode
        {
            get { return this.postalCode; }
            set { this.postalCode = value; }
        }

        /// <summary>
        /// Housing
        /// </summary>
        public string Housing
        {
            get { return this.housing; }
            set { this.housing = value; }
        }

        /// <summary>
        /// Number of Rooms
        /// </summary>
        public string NumberOfRooms
        {
            get { return this.numberOfRooms; }
            set { this.numberOfRooms = value; }
        }

        /// <summary>
        /// Number of Floors
        /// </summary>
        public string NumberOfFloors
        {
            get { return this.numberOfFloors; }
            set { this.numberOfFloors = value; }
        }

        /// <summary>
        /// The year that the property was built
        /// </summary>
        public string YearBuilt
        {
            get { return this.yearBuilt; }
            set { this.yearBuilt = value; }
        }

        /// <summary>
        /// Price
        /// </summary>
        public double Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        /// <summary>
        /// Payment
        /// </summary>
        public double Payment
        {
            get { return this.payment; }
            set { this.payment = value; }
        }

        /// <summary>
        /// Monthly Rate
        /// </summary>
        public double MonthlyRate
        {
            get { return this.monthlyRate; }
            set { this.monthlyRate = value; }
        }

        /// <summary>
        /// Net
        /// </summary>
        public double Net
        {
            get { return this.net; }
            set { this.net = value; }
        }

        /// <summary>
        /// Price / m2
        /// </summary>
        public double PriceSquareMeter
        {
            get { return this.priceSquareMeter; }
            set { this.priceSquareMeter = value; }
        }

        /// <summary>
        /// Latitude
        /// </summary>
        public float Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; }
        }

        /// <summary>
        /// Longitude
        /// </summary>
        public float Longitude
        {
            get { return this.longitude; }
            set { this.longitude = value; }
        }

        /// <summary>
        /// Agent ID
        /// </summary>
        public Guid AgentId
        {
            get { return this.agentId; }
            set { this.agentId = value; }
        }


        #endregion

        #region Fields

        private string itemNumber;
        private string address;
        private string postalCode;
        private string housing;
        private string numberOfRooms;
        private string numberOfFloors;
        private string yearBuilt;
        private double price;
        private double payment;
        private double monthlyRate;
        private double net;
        private double priceSquareMeter;
        private float latitude;
        private float longitude;
        private Guid agentId;

        #endregion
    }
}
