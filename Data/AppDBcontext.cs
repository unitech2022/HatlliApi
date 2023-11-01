using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.Models;
using HattliApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HattliApi.Data
{
    public class AppDBcontext : IdentityDbContext<User>
    {
        public AppDBcontext(DbContextOptions<AppDBcontext> options) : base(options)
        {



        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Provider>? Providers { get; set; }



        public DbSet<Address>? Addresses { get; set; }

        public DbSet<Cart>? Carts { get; set; }


        public DbSet<Alert>? Alerts { get; set; }

        public DbSet<Favorite>? Favorites { get; set; }

        public DbSet<Order>? Orders { get; set; }

        public DbSet<OrderItem>? OrderItems { get; set; }

        public DbSet<OrderWallet>? OrderWallets { get; set; }

        public DbSet<Setting>? Settings { get; set; }

        //  public DbSet<Market>? Markets { get; set; }

        public DbSet<Rate>? Rates { get; set; }

        public DbSet<ManualOrder>? ManualOrders { get; set; }



    }
}