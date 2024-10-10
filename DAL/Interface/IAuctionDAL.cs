using DTO;

namespace DAL.Interface
{
    public interface IAuctionDal
    {
        List<Auction> GetAll();
        Auction Insert(Auction auction);
        Auction GetById(int id);
        void Delete(int id); // Додаємо метод видалення
        void Update(Auction auction); // Додаємо метод оновлення
    }

}