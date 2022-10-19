using Database.Models;
using NetTopologySuite.Geometries;

namespace Database;

public static class DataSeeder
{
    public static void Seed(BurgerBackendDbContext context)
    {
        //temporary
        if (!context.Restaurants.Any()) {
            var restaurant1 = new Restaurant() { Id = Guid.NewGuid(), Name = "Cafe Vivaldi", ZipCode = 1069, Street = "Bremerholm 1", City = "København", OpeningHours = "Alle dage fra 10-22", Location = new Point(12.58287, 55.67964) { SRID = 4326 } };
            var restaurant2 = new Restaurant() { Id = Guid.NewGuid(), Name = "Bøff Burgerbar Amagertorv", ZipCode = 1060, Street = "Amagertorv", City = "København", OpeningHours = "Alle dage fra 10-22", Location = new Point(12.57897, 55.67898) { SRID = 4326 } };
            var restaurant3 = new Restaurant() { Id = Guid.NewGuid(), Name = "Sporvejen", ZipCode = 1154, Street = "Gråbrødretorv 17", City = "København", OpeningHours = "Alle dage fra 9-22", Location = new Point(12.57569, 55.67963) { SRID = 4326 } };
            var restaurant4 = new Restaurant() { Id = Guid.NewGuid(), Name = "Burger Shack", ZipCode = 1123, Street = "Gothersgade 15", City = "København", OpeningHours = "Alle dage fra 9-22", Location = new Point(12.58409, 55.68185) { SRID = 4326 } };
            var restaurant5 = new Restaurant() { Id = Guid.NewGuid(), Name = "Gasoline Grill", ZipCode = 1300, Street = "Landgreven 10", City = "København", OpeningHours = "Alle dage fra 9-22", Location = new Point(12.58508, 55.68344) { SRID = 4326 } };
            var restaurant6 = new Restaurant() { Id = Guid.NewGuid(), Name = "It's Burger", ZipCode = 1123, Street = "Gothersgade 40", City = "København", OpeningHours = "Alle dage fra 9-22", Location = new Point(12.58250, 55.68235) { SRID = 4326 } };
            var restaurant7 = new Restaurant() { Id = Guid.NewGuid(), Name = "Dandelion Burger", ZipCode = 1450, Street = "Nytorv 3", City = "København", OpeningHours = "Alle dage fra 11-22", Location = new Point(12.57329, 55.67787) { SRID = 4326 } };
            var restaurant8 = new Restaurant() { Id = Guid.NewGuid(), Name = "Cock's & Cows", ZipCode = 1202, Street = "Gammel Strand 34", City = "København", OpeningHours = "Alle dage fra 11-22", Location = new Point(12.57887, 55.67785) { SRID = 4326 } };
            var restaurant9 = new Restaurant() { Id = Guid.NewGuid(), Name = "Winner Burger", ZipCode = 1260, Street = "Bredgade 28", City = "København", OpeningHours = "Alle dage fra 11-22", Location = new Point(12.58985, 55.68288) { SRID = 4326 } };
            var restaurant10 = new Restaurant() { Id = Guid.NewGuid(), Name = "Jagger", ZipCode = 1150, Street = "Købmagergade 43", City = "København", OpeningHours = "Alle dage fra 11-22", Location = new Point(12.57708, 55.68080) { SRID = 4326 } };
            var restaurant11 = new Restaurant() { Id = Guid.NewGuid(), Name = "Bronx", ZipCode = 1467, Street = "Vandkunsten 1", City = "København", OpeningHours = "Alle dage fra 10-22", Location = new Point(12.57470, 55.67615) { SRID = 4326 } };
            var restaurant12 = new Restaurant() { Id = Guid.NewGuid(), Name = "MAX Burgers", ZipCode = 1457, Street = "Gammeltorv 4", City = "København", OpeningHours = "Alle dage fra 10-22", Location = new Point(12.57269, 55.67831) { SRID = 4326 } };
            var restaurant13 = new Restaurant() { Id = Guid.NewGuid(), Name = "The Burger Concept", ZipCode = 1360, Street = "Frederiksborggade 7", City = "København", OpeningHours = "Alle dage fra 10-22", Location = new Point(12.57275, 55.68307) { SRID = 4326 } };
            var restaurant14 = new Restaurant() { Id = Guid.NewGuid(), Name = "Halifax Hillerød", ZipCode = 3400, Street = "Torvet 6", City = "Hillerød", OpeningHours = "Alle dage fra 11:30-21", Location = new Point(12.30229, 55.92945) { SRID = 4326 } };
            var restaurant15 = new Restaurant() { Id = Guid.NewGuid(), Name = "Timeout Billund", ZipCode = 7190, Street = "Gammelbro 32a", City = "Billund", OpeningHours = "Alle dage fra 11:30-21", Location = new Point(9.11326, 55.73064) { SRID = 4326 } };
            var restaurant16 = new Restaurant() { Id = Guid.NewGuid(), Name = "Burger Kitchen Billund", ZipCode = 7190, Street = "Nordmarksvej 9", City = "Billund", OpeningHours = "Alle dage fra 12-21", Location = new Point(9.12659, 55.73434) { SRID = 4326 } };

            var adminUser = new User() { Id = Guid.NewGuid(), Name = "Bjarne S.", UserName = "admin", Password = "1234", Role = "admin" };
            var regularUser1 = new User() { Id = Guid.NewGuid(), Name = "Hanne A.", UserName = "test", Password = "1234" };
            var regularUser2 = new User() { Id = Guid.NewGuid(), Name = "Ole F.", UserName = "test1", Password = "1234" };
            var regularUser3 = new User() { Id = Guid.NewGuid(), Name = "Bitten X.", UserName = "test2", Password = "1234" };

            var rating1 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser1, Restaurant = restaurant1, TasteRating = 2, TextureRating = 4, VisualPresentationRating = 1, RatedAt = DateTime.Now };
            var rating1_2 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser2, Restaurant = restaurant1, TasteRating = 1, TextureRating = 3, VisualPresentationRating = 2, RatedAt = DateTime.Now };
            var rating2 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser1, Restaurant = restaurant2, TasteRating = 5, TextureRating = 3, VisualPresentationRating = 4, RatedAt = DateTime.Now };
            var rating3 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser2, Restaurant = restaurant3, TasteRating = 2, TextureRating = 5, VisualPresentationRating = 1, RatedAt = DateTime.Now };
            var rating4 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser1, Restaurant = restaurant4, TasteRating = 3, TextureRating = 4, VisualPresentationRating = 4, RatedAt = DateTime.Now };
            var rating5 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser2, Restaurant = restaurant5, TasteRating = 2, TextureRating = 3, VisualPresentationRating = 2, RatedAt = DateTime.Now };
            var rating6 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser1, Restaurant = restaurant6, TasteRating = 1, TextureRating = 5, VisualPresentationRating = 1, RatedAt = DateTime.Now };
            var rating7 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser1, Restaurant = restaurant7, TasteRating = 4, TextureRating = 4, VisualPresentationRating = 4, RatedAt = DateTime.Now };
            var rating8 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser2, Restaurant = restaurant8, TasteRating = 3, TextureRating = 1, VisualPresentationRating = 3, RatedAt = DateTime.Now };
            var rating9 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser1, Restaurant = restaurant9, TasteRating = 5, TextureRating = 2, VisualPresentationRating = 5, RatedAt = DateTime.Now };
            var rating10 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser1, Restaurant = restaurant10, TasteRating = 0, TextureRating =2, VisualPresentationRating = 5, RatedAt = DateTime.Now };
            var rating11 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser2, Restaurant = restaurant11, TasteRating = 2, TextureRating = 0, VisualPresentationRating = 3, RatedAt = DateTime.Now };
            var rating12 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser1, Restaurant = restaurant12, TasteRating = 3, TextureRating = 1, VisualPresentationRating = 2, RatedAt = DateTime.Now };
            var rating13 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser3, Restaurant = restaurant13, TasteRating = 1, TextureRating = 1, VisualPresentationRating = 1, RatedAt = DateTime.Now };
            var rating14 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser2, Restaurant = restaurant14, TasteRating = 4, TextureRating = 5, VisualPresentationRating = 4, RatedAt = DateTime.Now };
            var rating15 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser1, Restaurant = restaurant15, TasteRating = 3, TextureRating = 3, VisualPresentationRating = 2, RatedAt = DateTime.Now };
            var rating16 = new Rating() { Id = Guid.NewGuid(), RatedByUser = regularUser3, Restaurant = restaurant16, TasteRating = 5, TextureRating = 4, VisualPresentationRating = 3, RatedAt = DateTime.Now };

            context.Users.Add(adminUser);
            context.Users.Add(regularUser1);
            context.Users.Add(regularUser2);
            context.Users.Add(regularUser3);

            context.Restaurants.Add(restaurant1);
            context.Restaurants.Add(restaurant2);
            context.Restaurants.Add(restaurant3);
            context.Restaurants.Add(restaurant4);
            context.Restaurants.Add(restaurant5);
            context.Restaurants.Add(restaurant6);
            context.Restaurants.Add(restaurant7);
            context.Restaurants.Add(restaurant8);
            context.Restaurants.Add(restaurant9);
            context.Restaurants.Add(restaurant10);
            context.Restaurants.Add(restaurant11);
            context.Restaurants.Add(restaurant12);
            context.Restaurants.Add(restaurant13);
            context.Restaurants.Add(restaurant14);
            context.Restaurants.Add(restaurant15);
            context.Restaurants.Add(restaurant16);

            context.Ratings.Add(rating1);
            context.Ratings.Add(rating1_2);
            context.Ratings.Add(rating2);
            context.Ratings.Add(rating3);
            context.Ratings.Add(rating4);
            context.Ratings.Add(rating5);
            context.Ratings.Add(rating6);
            context.Ratings.Add(rating7);
            context.Ratings.Add(rating8);
            context.Ratings.Add(rating9);
            context.Ratings.Add(rating10);
            context.Ratings.Add(rating11);
            context.Ratings.Add(rating12);
            context.Ratings.Add(rating13);
            context.Ratings.Add(rating14);
            context.Ratings.Add(rating15);
            context.Ratings.Add(rating16);

            context.SaveChanges();
        }
    }
}
