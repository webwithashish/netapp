using System;
using System.Collections.Generic;
using System.Linq;
using Infosys.DBFirstCore.DataAccessLayer.Models;

namespace Infosys.DBFirstCore.DataAccessLayer
{
    public class PricingRepository
    {
        private PricingDbContext context;

        public PricingDbContext Context { get { return context; } }

        public PricingRepository(PricingDbContext dbContext)
        {
            context = dbContext;
        }

        public PricingRepository()
        {
            context = new PricingDbContext();
        }

        #region Products

        public List<Product> GetAllProducts()
        {
            try
            {
                return context.Products.OrderBy(p => p.ProductId).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                return context.Products.FirstOrDefault(p => p.ProductId == productId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddProduct(Product product)
        {
            bool status = false;
            try
            {
                context.Products.Add(product);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool UpdateProduct(Product product)
        {
            bool status = false;
            try
            {
                var prod = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (prod != null)
                {
                    prod.Name = product.Name;
                    prod.Description = product.Description;
                    prod.BasePrice = product.BasePrice;
                    prod.IsActive = product.IsActive;
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteProduct(int productId)
        {
            bool status = false;
            try
            {
                var prod = context.Products.FirstOrDefault(p => p.ProductId == productId);
                if (prod != null)
                {
                    context.Products.Remove(prod);
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        #endregion

        #region Markers

        public List<Marker> GetAllMarkers()
        {
            try
            {
                return context.Markers.OrderBy(m => m.MarkerId).ToList();
            }
            catch (Exception)
            {
                return new List<Marker>();
            }
        }

        public Marker GetMarkerById(int markerId)
        {
            try
            {
                return context.Markers.FirstOrDefault(m => m.MarkerId == markerId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddMarker(Marker marker)
        {
            try
            {
                context.Markers.Add(marker);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMarker(Marker marker)
        {
            try
            {
                var m = context.Markers.FirstOrDefault(x => x.MarkerId == marker.MarkerId);
                if (m != null)
                {
                    m.Name = marker.Name;
                    m.Description = marker.Description;
                    m.IsActive = marker.IsActive;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool DeleteMarker(int markerId)
        {
            try
            {
                var m = context.Markers.FirstOrDefault(x => x.MarkerId == markerId);
                if (m != null)
                {
                    context.Markers.Remove(m);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        #endregion

        #region MarkerPrices

        public List<MarkerPrice> GetMarkerPricesByMarker(int markerId)
        {
            try
            {
                return context.MarkerPrices
                              .Where(mp => mp.MarkerId == markerId)
                              .OrderBy(mp => mp.ValidFrom)
                              .ToList();
            }
            catch (Exception)
            {
                return new List<MarkerPrice>();
            }
        }

        public MarkerPrice GetMarkerPriceById(int markerPriceId)
        {
            try
            {
                return context.MarkerPrices.FirstOrDefault(mp => mp.MarkerPriceId == markerPriceId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddMarkerPrice(MarkerPrice markerPrice)
        {
            try
            {
                context.MarkerPrices.Add(markerPrice);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMarkerPrice(MarkerPrice markerPrice)
        {
            try
            {
                var mp = context.MarkerPrices.FirstOrDefault(x => x.MarkerPriceId == markerPrice.MarkerPriceId);
                if (mp != null)
                {
                    mp.Value = markerPrice.Value;
                    mp.ValidFrom = markerPrice.ValidFrom;
                    mp.ValidTo = markerPrice.ValidTo;
                    mp.IsActive = markerPrice.IsActive;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool DeleteMarkerPrice(int markerPriceId)
        {
            try
            {
                var mp = context.MarkerPrices.FirstOrDefault(x => x.MarkerPriceId == markerPriceId);
                if (mp != null)
                {
                    context.MarkerPrices.Remove(mp);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        #endregion

        #region ProductDiffs

        public List<ProductDiff> GetProductDiffsByProduct(int productId)
        {
            try
            {
                return context.ProductDiffs
                              .Where(pd => pd.ProductId == productId)
                              .ToList();
            }
            catch (Exception)
            {
                return new List<ProductDiff>();
            }
        }

        public bool AddProductDiff(ProductDiff diff)
        {
            try
            {
                context.ProductDiffs.Add(diff);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateProductDiff(ProductDiff diff)
        {
            try
            {
                var pd = context.ProductDiffs.FirstOrDefault(x => x.ProductDiffId == diff.ProductDiffId);
                if (pd != null)
                {
                    pd.DiffValue = diff.DiffValue;
                    pd.IsActive = diff.IsActive;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool DeleteProductDiff(int productDiffId)
        {
            try
            {
                var pd = context.ProductDiffs.FirstOrDefault(x => x.ProductDiffId == productDiffId);
                if (pd != null)
                {
                    context.ProductDiffs.Remove(pd);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public decimal CalculateFinalPrice(int productId, DateTime asOfDate)
        {
            try
            {
                var product = context.Products.FirstOrDefault(p => p.ProductId == productId && p.IsActive);
                if (product == null) return 0;

                var diffs = (from pd in context.ProductDiffs
                             join mp in context.MarkerPrices on pd.MarkerId equals mp.MarkerId
                             where pd.ProductId == productId
                                   && pd.IsActive
                                   && mp.IsActive
                                   && mp.ValidFrom.ToDateTime(TimeOnly.MinValue) <= asOfDate
                                   && (mp.ValidTo == null || mp.ValidTo.Value.ToDateTime(TimeOnly.MaxValue) >= asOfDate)
                             select pd.DiffValue + mp.Value).ToList();

                return product.BasePrice + (diffs.Any() ? diffs.Sum() : 0);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion
    }
}
