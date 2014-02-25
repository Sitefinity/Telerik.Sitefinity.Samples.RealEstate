using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.StarterKit.Modules.RealEstate.Model;

namespace Telerik.StarterKit.Modules.RealEstate.Data
{
    public enum SinglePhotoType
    {
        SliderPhoto,
        SliderThumbnail,
        FlowListPhoto,
        ThumbnailListPhoto,
        SimilarPropertiesPhoto
    }

    public enum MultiplePhotoType
    {
        OverviewTabPhoto,
        PhotosTabPhoto,
        PanaromicViewTabPhoto,
        FloorPlanTabPhoto
    }

    public enum TaxonType
    {
        Types, // flat types
        Features,
        Rooms,
        Locations
    }

    internal static class RealEstateItemHelper
    {
        private static LibrariesManager s_LibrariesManager;
        private static TaxonomyManager s_TaxonomyManager;

        static RealEstateItemHelper()
        {
            s_LibrariesManager = new LibrariesManager();
            s_TaxonomyManager = new TaxonomyManager();

            s_LibrariesManager.Provider.SuppressSecurityChecks = true;

        }

        public static T GetTaxon<T>(this RealEstateItem realEstateItem, TaxonType taxonType) where T : Taxon
        {
            var ids = (TrackedList<Guid>)realEstateItem.GetValue(taxonType.ToString());

            if (ids != null && ids.Count > 0)
            {
                return s_TaxonomyManager.GetTaxon<T>(ids[0]);
            }

            return null;
        }

        public static List<T> GetTaxons<T>(this RealEstateItem realEstateItem, TaxonType taxonType) where T : Taxon
        {
            List<T> result = new List<T>();
            var ids = (TrackedList<Guid>)realEstateItem.GetValue(taxonType.ToString());
            foreach (var id in ids)
            {
                result.Add(s_TaxonomyManager.GetTaxon<T>(id));
            }

            return result;
        }

        public static bool IsForSale(this RealEstateItem realEstateItem)
        {
            bool result = false;

                if ((realEstateItem.GetValue<TrackedList<Guid>>("Category")).Contains(RealEstateModule.ForSaleItemTypeTaxonId))
                {
                    result = true;
                }

            return result;
        }

        public static List<Photo> GetPhotos(this RealEstateItem realEstateItem, MultiplePhotoType photoType)
        {
            List<Photo> photos = null;

            var itemAlbum = App.Prepare().WorkWith().Albums()
                .Where(album => album.Title.Equals(realEstateItem.ItemNumber, StringComparison.OrdinalIgnoreCase))
                .Get().FirstOrDefault();

            if (itemAlbum != null)
            {
                var allPhotos = itemAlbum.Images()
                    .Where(i => i.Status == Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live);

                Guid tagId = Guid.Empty;

                if (photoType == MultiplePhotoType.OverviewTabPhoto)
                {
                    tagId = RealEstateModule.OverviewTabPhotoTaxonId;
                }
                else if (photoType == MultiplePhotoType.PhotosTabPhoto)
                {
                    tagId = RealEstateModule.PhotosTabPhotoTaxonId;
                }
                else if (photoType == MultiplePhotoType.PanaromicViewTabPhoto)
                {
                    tagId = RealEstateModule.PanaromicViewTabPhotoTaxonId;
                }
                else if (photoType == MultiplePhotoType.FloorPlanTabPhoto)
                {
                    tagId = RealEstateModule.FloorPlanTabPhotoTaxonId;
                }

                if (!tagId.Equals(Guid.Empty))
                {
                    photos = (
                                from p in allPhotos.Where(p => (p.GetValue<TrackedList<Guid>>("Category")).Contains(tagId))
                                select new Photo
                                {
                                    Id = p.Id,
                                    Title = p.Title,
                                    Description = p.Description,
                                    Url = string.Format("{0}{1}", s_LibrariesManager.GetItemUrl(p), p.Extension)
                                }
                              )
                              .ToList();
                }
                else
                {
                    // initialize to an empty list of photos
                    photos = new List<Photo>(0);
                }

            }
            return photos;
        }

        public static string GetPhotoUrl(this RealEstateItem realEstateItem, SinglePhotoType photoType)
        {
            string url = string.Empty;

            var itemAlbum = App.Prepare().WorkWith().Albums()
                .Where(album => album.Title.Equals(realEstateItem.ItemNumber, StringComparison.OrdinalIgnoreCase))
                .Get().FirstOrDefault();

            if (itemAlbum != null)
            {
                var allPhotos = itemAlbum.Images()
                    .Where(i => i.Status == Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live);

                Guid tagId = Guid.Empty;

                if (photoType == SinglePhotoType.SliderPhoto)
                {
                    tagId = RealEstateModule.SliderPhotoTaxonId;
                }
                else if (photoType == SinglePhotoType.SliderThumbnail)
                {
                    tagId = RealEstateModule.SliderThumbnailTaxonId;
                }
                else if (photoType == SinglePhotoType.FlowListPhoto)
                {
                    tagId = RealEstateModule.FlowListPhotoTaxonId;
                }
                else if (photoType == SinglePhotoType.ThumbnailListPhoto)
                {
                    tagId = RealEstateModule.ThumbnailListPhotoTaxonId;
                }
                else if (photoType == SinglePhotoType.SimilarPropertiesPhoto)
                {
                    tagId = RealEstateModule.SimilarPropertiesPhotoTaxonId;
                }

                if (!tagId.Equals(Guid.Empty))
                {
                    var photo = allPhotos
                            .Where(p => (p.GetValue<TrackedList<Guid>>("Category")).Contains(tagId))
                            .Take(1)
                            .FirstOrDefault();

                    if (photo != null)
                    {
                        url = string.Format("{0}{1}", s_LibrariesManager.GetItemUrl(photo), photo.Extension);
                    }
                }
            }

            return url;
        }
    }
}
