using DTO;

namespace DAL.Interface
{
    public interface IProductDal
    {
        List<Product> GetAll(); // Отримати всі продукти
        Product GetById(int id); // Отримати продукт за ID
        Product Insert(Product product); // Вставити новий продукт
        void Update(Product product); // Оновити існуючий продукт
        void Delete(int id); // Видалити продукт за ID
    }
}
