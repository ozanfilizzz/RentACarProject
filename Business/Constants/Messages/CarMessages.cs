using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class CarMessages
    {
        public static string CarAdded = "Araç başarıyla eklendi.";
        public static string CarUpdated = "Araç başarıyla güncellendi.";
        public static string CarDeleted = "Araç başarıyla silindi.";
        public static string CarNameInValid = "Araç ismi geçersiz.";
        public static string MaintenanceTime = "Sistem bakımda. İşlem başarısız.";
        public static string CarsListed= "Araçlar başarıyla listelendi.";
        public static string CarNameAlreadyExists= "Ürün isimleri aynı olamaz.";
        public static string CarCountOfCategoryError= "Bir kategoride en fazla 10 ürün olabilir.";
        public static string CategoryLimitExceded= "Kategori limiti aşılmıştır. Yeni kategori eklenemez.";
    }
}
