# LLM Kullanım Dokümantasyonu

## Özet

| Bilgi | Değer |
|-------|-------|
| Toplam prompt sayısı |  |
| Kullanılan araçlar | Gemini / ChatGPT |
| En çok yardım alınan konular | Mimari Tasarım, Code Review, C# Standards |
| Tahmini LLM ile kazanılan süre |  |

---

## Prompt 1: Core Interaction Mimarisi

**Araç:** Gemini
**Tarih/Saat:** 2025-01-31 02.53

**Prompt:**
> "Unity'de modüler bir Interaction System kurmak istiyorum. Ludu Arts standartlarına (SOLID, Clean Code) uygun olarak; Interface, Base Class ve Enum yapısını nasıl kurgulamalıyım? IInteractable arayüzü neleri içermeli?"

**Alınan Cevap (Özet):**
> InteractionType enum'ı (Instant, Hold, Toggle), IInteractable interface'i ve MonoBehaviour gereksinimlerini karşılayan abstract InteractableBase sınıfı önerildi. Kod örnekleri C# coding convention'larına (Region, XML docs) uygun olarak verildi.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Temel mimariyi hatasız kurmak için base yapıları direkt aldım.

---

## Prompt 2: InteractionDetector Logic ve Code Review

**Araç:** Gemini
**Tarih/Saat:** 2025-01-31 03.17

**Prompt:**
> "Yazdığım InteractionDetector sınıfını Ludu Arts kod standartları, olası mantık hataları (bug) ve 'm_' prefix kullanımı açısından review eder misin? Özellikle Raycast boşluğa geldiğinde seçimi kaldırma mantığı doğru mu?"

**Alınan Cevap (Özet):**
> Kodda Raycast'in IInteractable olmayan objelere çarptığında seçimi temizlemediği (logic bug) tespit edildi. Toggle interaction eksikliği belirtildi. Event isimlendirmeleri ve null-check optimizasyonları ile kod refactor edildi.

**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Logic hatasını düzelttim ve önerilen event yapısını kendi koduma entegre ettim.

---

## Prompt 3: C# Namespace Standartları

**Araç:** Gemini
**Tarih/Saat:** 2025-01-31 03.20

**Prompt:**
> "Proje dosya adım 'Ludu_Arts_Case' ancak C# standartlarında namespace PascalCase olmalı. Mevcut projede namespace isimlendirme standartlarını uygulamak için güvenli refactoring yöntemi nedir? GitHub geçmişini bozmadan nasıl yaparım?"

**Alınan Cevap (Özet):**
> Namespace'in klasör adından bağımsız olabileceği belirtildi. IDE'nin 'Replace in Files' özelliği ile namespace'lerin 'LuduArtsCase' olarak güncellenmesi önerildi. Bunun clean code için önemli olduğu vurgulandı.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Kod kalitesini artırmak için namespace düzenlemesini uyguladım.

---

## Prompt 4: ScriptableObject Inventory Mimarisi

**Araç:** Gemini
**Tarih/Saat:** 2025-01-31 03.40

**Prompt:**
> "Veri (Data) ve Mantığı (Logic) ayırmak için ScriptableObject tabanlı, genişletilebilir bir Inventory sistemi mimarisi nasıl tasarlanmalı? ItemData, InventoryController ve WorldPickup arasındaki ilişkiyi açıklar mısın?"

**Alınan Cevap (Özet):**
> 3 parçalı bir yapı önerildi: 1) ItemData (ScriptableObject) ile veri tanımı, 2) InventoryController ile liste yönetimi ve eventler, 3) ItemPickup (InteractableBase) ile dünya etkileşimi. HasItem kontrolü için ID bazlı arama önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Mimarinin mantığını anlayıp kendi kod yapımda uyguladım.

---

## Prompt 5: ScriptableObject ve Inventory Kodlama

**Araç:** Gemini
**Tarih/Saat:** 2025-01-31 04.18

**Prompt:**
> "Inventory sistemi için ItemData (ScriptableObject), InventoryController ve ItemPickup sınıflarını yazdım. Kodları Ludu Arts standartlarına, encapsulation kurallarına ve güvenli ID kontrolüne göre refactor eder misin?"

**Alınan Cevap (Özet):**
> Yazılan kodlarda public field kullanımı (encapsulation ihlali) ve isim tabanlı ID kontrolü (güvenlik riski) tespit edildi. Kodlar 'm_' prefixleri, Property kullanımı ve Item.ID tabanlı doğrulama ile refactor edilerek sunuldu.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Kodun daha güvenli ve standartlara uygun olması için revize edilmiş versiyonları kullandım.

---

## Prompt 6: Inventory Debugging (String Mismatch)

**Araç:** Gemini
**Tarih/Saat:** 2025-01-31 05.00

**Prompt:**
> "Anahtarı almama rağmen kapı hala 'Locked' diyor ve anahtarı görmüyor. Sorun nerede olabilir?"

**Alınan Cevap (Özet):**
> Sorunun ItemData ID'si ile Kapıdaki KeyID arasındaki string uyuşmazlığından (boşluk karakteri veya isim vs ID karışıklığı) kaynaklanabileceği belirtildi. Debug.Log ile trace etme ve .Trim() kullanımı önerildi.

**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Hata ayıklama adımlarını takip ederek Inspector'daki ID uyuşmazlığını düzelttim.

---

## Prompt 7: Chest (Sandık) Mekaniği

**Araç:** Gemini
**Tarih/Saat:** 2025-01-31 06.30

**Prompt:**
> "InteractionType.Hold mekaniğini kullanan, açıldığında Item veren ve animasyon oynatan Chest sınıfını yazıyorum. Yazdığım kodu Ludu Arts standartlarına göre review eder misin? InteractableBase'deki private değişkenlere erişim sorununu nasıl çözmeliyim?"

**Alınan Cevap (Özet):**
> Kodda Region eksiklikleri ve Encapsulation ihlali (Base class private field erişimi) tespit edildi. Base class'a 'SetPromptText' metodu eklenmesi ve SmoothStep animasyon kullanılması önerildi. Kod standartlara uygun hale getirildi.

**Nasıl Kullandım:**
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Base class yapısını bozmadan inheritance kurallarına uygun setter metotlar ekledim ve kodu güncelledim.

---

## Prompt 8: UI Implementation & Optimization

**Araç:** Gemini
**Tarih/Saat:** 2025-01-31 07.38

**Prompt:**
> "InteractionUI scriptini yazdım, çalışıyor ancak oyunun başında ilk etkileşimde kısa bir donma (stutter) oluyor. Ayrıca kodumun Ludu Arts standartlarına (Memory Management, Clean Code) uygunluğunu kontrol eder misin?"

**Alınan Cevap (Özet):**
> Donma sorununun Canvas Rebuild veya ilk Texture yüklemesinden kaynaklandığı belirtildi. Kodda 'OnDisable' içinde event aboneliğinden çıkılmadığı (Memory Leak riski) tespit edildi. GameObject yerine direkt Component referansı kullanılarak ve event unsubscribe eklenerek kod optimize edildi.

**Nasıl Kullandım:**
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Event yönetimindeki hatayı düzelttim ve referans yönetimini optimize edilmiş kod ile değiştirdim.