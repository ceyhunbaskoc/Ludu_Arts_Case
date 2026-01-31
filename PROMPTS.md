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
**Tarih/Saat:** 2023-10-27 10:00 (Tarihi güncelleyin)

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
**Tarih/Saat:** 2023-10-27 10:30

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
**Tarih/Saat:** 2023-10-27 11:00

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
**Tarih/Saat:** 2023-10-27 11:15

**Prompt:**
> "Veri (Data) ve Mantığı (Logic) ayırmak için ScriptableObject tabanlı, genişletilebilir bir Inventory sistemi mimarisi nasıl tasarlanmalı? ItemData, InventoryController ve WorldPickup arasındaki ilişkiyi açıklar mısın?"

**Alınan Cevap (Özet):**
> 3 parçalı bir yapı önerildi: 1) ItemData (ScriptableObject) ile veri tanımı, 2) InventoryController ile liste yönetimi ve eventler, 3) ItemPickup (InteractableBase) ile dünya etkileşimi. HasItem kontrolü için ID bazlı arama önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Mimarinin mantığını anlayıp kendi kod yapımda uyguladım.