using AutoMapper;
using HattliApi.Data;
using HattliApi.Models;
using HattliApi.Models.BaseEntity;
using Microsoft.EntityFrameworkCore;
using HattliApi.ViewModels;
using X.PagedList;
using HatlliApi.ViewModels;
namespace HatlliApi.Serveries.RateServices
{
    public class RateServices : IRateServices
    {
        private readonly IMapper _mapper;


        private readonly AppDBcontext _context;

        public RateServices(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<dynamic> AddAsync(dynamic type)
        {
            await _context.Addresses!.AddAsync(type);

            await _context.SaveChangesAsync();

            return type;
        }

        public async Task<dynamic> AddRate(Rate rate)
        {
            Rate? checkRate = await _context.Rates!.FirstOrDefaultAsync(t => t.UserId == rate.UserId && t.ProductId == rate.ProductId);
            Product? product = await _context.Products!.FirstOrDefaultAsync(t => t.Id == rate.ProductId);
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == product!.ProviderId);
            if (checkRate == null)
            {

                await _context.Rates!.AddAsync(rate);
                _context.SaveChanges();
                List<Rate> rates = await _context.Rates!.Where(t => t.ProductId == rate.ProductId).ToListAsync();


                // ** rate ptoduct
                //culact rate WorkShop
                int rateConte = rates.Count();
                Console.WriteLine("rateConte" + rateConte);
                int stars = rates.Sum(t => t.Stare);
                Console.WriteLine("stars" + stars);
                double totalRate = stars / rateConte;
                Console.WriteLine("rate" + totalRate);
                product!.Rate = totalRate;




                //** rate provider
                //culact rate WorkShop
                List<Product> products = await _context.Products!.Where(t => t.ProviderId == product.ProviderId).ToListAsync();
                int rateConteProvider = products.Count();
                Console.WriteLine("rateConte" + rateConteProvider);
                double starsProvider = products.Sum(t => t.Rate);
                Console.WriteLine("stars" + stars);
                double totalRateProvider = starsProvider / rateConteProvider;
                Console.WriteLine("rate" + totalRateProvider);
                provider!.Rate = totalRateProvider;

                await _context.SaveChangesAsync();

                return new
                {
                    message = "تم التقييم بنجاح ",
                    rate = rate
                };
            }
            else
            {
                List<Rate> rates = await _context.Rates!.Where(t => t.ProductId == rate.ProductId).ToListAsync();
                checkRate.Stare = rate.Stare;
                 checkRate.Comment = rate.Comment;
                int rateConte = rates.Count();
                // Console.WriteLine("rateConte"+rateConte);
                int stars = rates.Sum(t => t.Stare);
                // Console.WriteLine("stars"+stars);
                double totalRate = stars / rateConte;
                Console.WriteLine("rate" + totalRate);
                product!.Rate = totalRate;
                await _context.SaveChangesAsync();


                return new
                {
                    message = "تم تعديل التقييم ",
                    rate = rate
                };
            }

        }

        public async Task<dynamic> DeleteAsync(int typeId)
        {
            Rate? address = await _context.Rates!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (address != null)
            {
                _context.Rates!.Remove(address);

                await _context.SaveChangesAsync();
            }

            return address!;
        }

        public async Task<dynamic> GetItems(string UserId, int page)
        {
            List<Address> addresses = await _context.Addresses!.OrderByDescending(t => t.DefaultAddress).Where(i => i.UserId == UserId).ToListAsync();

            //  if(addresses.Count > 0){
            //     Address? defaultAddress= addresses!.FirstOrDefault(t => t.DefaultAddress=true);
            //     if(defaultAddress != null){
            //         addresses.Remove(defaultAddress);
            //         // addresses.Insert(1,defaultAddress);
            //     }
            //  }

            var pageResults = 20f;
            var pageCount = Math.Ceiling(addresses.Count() / pageResults);

            var items = await addresses
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();



            BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;
        }

        public Task<dynamic> GitById(int typeId)
        {
            throw new NotImplementedException();
        }


        public async Task<List<RateProductResponse>> GetRatesByProductId(int productId)
        {
          List<RateProductResponse> rats=new List<RateProductResponse>();

            List<Rate> allRates = await _context.Rates!.OrderByDescending(t => t.CreateAte).Where(i => i.ProductId == productId).ToListAsync();
            foreach (Rate item in allRates)
            {
                User? user=await _context.Users.FirstOrDefaultAsync(t=> t.Id==item.UserId);
                UserDetailResponse userDetails= _mapper.Map<UserDetailResponse>(user);
                RateProductResponse rateProductResponse=new RateProductResponse{
                    rate=item,
                    user=userDetails
                };
                rats.Add(rateProductResponse);
            }
            return rats;
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateObject(dynamic category)
        {
            throw new NotImplementedException();
        }
    }
}