
# Stock Management

Ürünler için stok yönetimini yapmak amacıyla tasarlanmıştır. Aşağıdaki kullanım senaryolarına yanıt vermektedir.

- Yeni ürün oluşturmak
- Ürünün stokta yer alan sayısını arttırmak
- Ürünün srokta yer alan sayısını azaltmak
- Ürünü stoktan kaldırmak

### _Yeni ürün oluşturmak_

Bu projede yer alan `ProductModel` sınıfı, var olmayan ProductManagement projesinde yer alan ürünlerin bir yansımasıdır. ProductManagement projesinde bir ürün yaratıldığı zaman atılacak entegrasyon event'i üzerinden StockManagement projesi içerisinde bir ürün yaratılmaktadır. Üretilen ürünler için _InitializeStock_ eylemi veri tabanına kaydedilmekte ardından da `StockSnapShotModel` verisi oluşturulup kaydedilmektedir. Bu işlemlerin ardından `StockSnapShotCreatedIntegrationEvent` sınıfının bir örneği üretilmekte ve sistemin geneline yayınlanmaktadır.

`StockSnapShotCreatedIntegrationEvent` entegrasyon eventini izleyenlerden biri StockManagement projesinin ta kendisidir. Bu şekilde `StockModel` verisini oluşturmaktadır. Bu model sadece okuma eylemleri için kullanılacaktır.

### _Ürünün stokta yer alan sayısını arttırmak_

ProductId, stoğa kaç adet ürün ekleneceği ve CorrelationId değeri gerekmektedir. CorrelationId işlemin sadece bir kere yürütülmesini sağlamak için kullanılmaktadır. __AddToStock__ eylemi tamamlandığı zaman `StockCountIncreasedIntegrationEvent` entegrasyon eventi sistem geneline yayınlanır.

`StockCountIncreasedIntegrationEvent` eventi StockManagement tarafından takip edilmektedir. Bu şekilde `StockModel` üzerinde yer alan _AvailableStock_ verisi güncellenmektedir.

### _Ürünün srokta yer alan sayısını azaltmak_

ProductId, stoktan kaç adet ürün çıkarılacağı ve CorrelationId değeri gerekmektedir. CorrelationId işlemin sadece bir kere yürütülmesini sağlamak için kullanılmaktadır. __RemoveFromStock__ eylemi tamamlandığı zaman `StockCountDecreasedIntegrationEvent` entegrasyon eventi sistem geneline yayınlanır.

`StockCountDecreasedIntegrationEvent` eventi StockManagement tarafından takip edilmektedir. Bu şekilde `StockModel` üzerinde yer alan _AvailableStock_ verisi güncellenmektedir.

### _Ürünü stoktan kaldırmak_

ProductId ve CorrelationId değeri gerekmektedir. CorrelationId işlemin sadece bir kere yürütülmesini sağlamak için kullanılmaktadır. __ResetStock__ eylemi temel de stoktan ürün çıkarmak ile aynı işlevi görmektedir. Bu sebeple işlemin sonunda `StockCountDecreasedIntegrationEvent` fırlatılmaktadır.

## __RUN__

__Way 1__

_docker-compose_ üzerinden projeyi çalıştırabilirsiniz. Debug yapabilen bir IDE'ye sahipseniz ana dizinde yer alan _docker-compose.yml_ dosyası işinize yarayacaktır.

Bu şekilde çalıştırmanız durumunda uygulamaya _http://localhost:2020_ üzerinden erişebirsiniz. _http://localhost:2020_ üzerine atılan istek sizi doğrudan swagger ekranına yönlendirecektir.

SqlServer, uygulamadan daha geç ayağa kalktığı için, `docker-compose up` işleminden sonra uygulamaya erişmeniz biraz vakit alabilir. 

__Way 2__

Docker kullanmadan debug işlemi yapmak istiyorsanız StockManagement/appsettings.json içerisinde yer alan bağlantı bilgilerini düzenlemeniz gerekecektir.

1) DbConfig -> DbOptions -> ConnectionStr değeri sahip olduğunuz bir SqlServer bağlantı verisi ile değiştirilmelidir.
2) DistributedLockConfig -> DistributedLockOptions -> ConnectionStr değeri sahip olduğunuz bir SqlServer bağlantı verisi ile değiştirilmelidir.
3) MassTransitConfig -> MassTransitOptions altınde yer alan HostName, VirtualHost, Username, Password verileri sahip olduğunuz bir RabbitMq platformuna ait verilerle değiştirilmelidir.

1. maddede yer alan bağlantı bilgisi yanlış olursa uygulama ayağa kalkamayacaktır. Geri kalan ayarların doğru olup olmadığını kontrol etmek içinse http://localhost/healthchecks_ui endpoint'inden yararlanabilirsiniz.

<img src="./images/StockManagement_HealthChecks_UI.png" alt="" width="500px"/>
