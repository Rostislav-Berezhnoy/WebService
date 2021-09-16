using System;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models
{
    public class AppTestContext : DbContext
    {
        public AppTestContext(DbContextOptions<AppTestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<SubOrganisation> SubOrganisation { get; set; }
        public DbSet<ConsumptionObject> ConsumptionObject { get; set; }
        public DbSet<ElectricitySupplyPoint> ElectricitySupplyPoint { get; set; }
        public DbSet<MeteringDevice> MeteringDevice { get; set; }
        public DbSet<ElectricityMeteringPoint> ElectricityMeteringPoint { get; set; }
        public DbSet<ElectricityMeter> ElectricityMeter { get; set; }
        public DbSet<ElectricalTransformer> ElectricalTransformer { get; set; }
        public DbSet<VoltageTransformer> VoltageTransformer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElectricityMeteringPoint>()
                .HasOne(a => a.ElectricalTransformers).WithOne(b => b.ElectricityMeteringPoint)
                .HasForeignKey<ElectricalTransformer>(e => e.ElectricityMeteringPointID);

            modelBuilder.Entity<ElectricityMeteringPoint>()
                .HasOne(a => a.ElectricityMeters).WithOne(b => b.ElectricityMeteringPoint)
                .HasForeignKey<ElectricityMeter>(e => e.ElectricityMeteringPointID);

            modelBuilder.Entity<ElectricityMeteringPoint>()
                .HasOne(a => a.VoltageTransformers).WithOne(b => b.ElectricityMeteringPoint)
                .HasForeignKey<VoltageTransformer>(e => e.ElectricityMeteringPointID);

            /* Наполнение тестовыми данными */

            //Расчетный прибор учета
            modelBuilder.Entity<MeteringDevice>().HasData(
                new MeteringDevice[] 
                {
                    new MeteringDevice {ID=1, No="123", StartDate=new DateTime(2018, 12, 01)},
                    new MeteringDevice {ID=2, No="456", StartDate=new DateTime(2018, 08, 15)},
                    new MeteringDevice {ID=3, No="789", StartDate=new DateTime(2019, 03, 18)}
                });

            //Счетчик электрической энергии
            modelBuilder.Entity<ElectricityMeter>().HasData(
                new ElectricityMeter[] 
                {
                    new ElectricityMeter {ID=1, No="123", Type="A1", VerificationDate=new DateTime(2020, 01, 13), ElectricityMeteringPointID=1},
                    new ElectricityMeter {ID=2, No="456", Type="B1", VerificationDate=new DateTime(2021, 02, 14), ElectricityMeteringPointID=2},
                    new ElectricityMeter {ID=3, No="789", Type="C1", VerificationDate=new DateTime(2022, 03, 15), ElectricityMeteringPointID=3},
                });

            //Трансформатор тока
            modelBuilder.Entity<ElectricalTransformer>().HasData(
                new ElectricalTransformer[] 
                {
                    new ElectricalTransformer {ID=1, No="123", Type="A1", VerificationDate=new DateTime(2020, 01, 13), TransformationRatio=1.1f, ElectricityMeteringPointID=1},
                    new ElectricalTransformer {ID=2, No="456", Type="B1", VerificationDate=new DateTime(2021, 02, 14), TransformationRatio=1.2f, ElectricityMeteringPointID=2},
                    new ElectricalTransformer {ID=3, No="789", Type="C1", VerificationDate=new DateTime(2022, 03, 15), TransformationRatio=1.3f, ElectricityMeteringPointID=3},
                });

            //Трансформатор напряжения
            modelBuilder.Entity<VoltageTransformer>().HasData(
                new VoltageTransformer[] 
                {
                    new VoltageTransformer {ID=1, No="123", Type="A1", VerificationDate=new DateTime(2020, 01, 13), TransformationRatio=1.1f, ElectricityMeteringPointID=1},
                    new VoltageTransformer {ID=2, No="456", Type="B1", VerificationDate=new DateTime(2021, 02, 14), TransformationRatio=1.2f, ElectricityMeteringPointID=2},
                    new VoltageTransformer {ID=3, No="789", Type="C1", VerificationDate=new DateTime(2022, 03, 15), TransformationRatio=1.3f, ElectricityMeteringPointID=3},
                });

            //Точка измерения электроэнергии
            modelBuilder.Entity<ElectricityMeteringPoint>(b =>{
                b.HasData(new ElectricityMeteringPoint[] 
                {
                    new ElectricityMeteringPoint {ID=1, Name="Точка измерения 1", ConsumptionObjectID=1, ElectricityMeterID=1},
                    new ElectricityMeteringPoint {ID=2, Name="Точка измерения 2", ConsumptionObjectID=1, ElectricityMeterID=2},
                    new ElectricityMeteringPoint {ID=3, Name="Точка измерения 3", ConsumptionObjectID=2, ElectricityMeterID=3},
                });
            });

            //Объект потребления
            modelBuilder.Entity<ConsumptionObject>(b => 
            {
                b.HasData(new ConsumptionObject[] 
                {
                    new ConsumptionObject {ID=1, Name="ПС 110/10 Весна", Address="Москва"},
                    new ConsumptionObject {ID=2, Name="ПС 220/20 Очень", Address="Воронеж"}
                });
            });
        }
    }
}