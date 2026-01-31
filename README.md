# Interaction System - [Ceyhun Başkoç]

> Ludu Arts Unity Developer Intern Case

## Proje Bilgileri

| Bilgi | Değer |
|-------|-------|
| Unity Versiyonu | 6000.0.063f1 (LTS) |
| Render Pipeline | URP |
| Case Süresi | 12 Saat |
| Tamamlanma Oranı | %100 |

---

## Kurulum

1. Repository'yi klonlayın:
```bash
git clone https://github.com/ceyhunbaskoc/Ludu_Arts_Case.git
````

2. Unity Hub'da projeyi açın.
    
3. `Assets/Ludu_Arts_Case/Scenes/TestScene.unity` sahnesini açın.
    
4. Play tuşuna basın.
    

---

## Nasıl Test Edilir

### Kontroller

|**Tuş**|**Aksiyon**|
|---|---|
|WASD|Karakter Hareketi|
|Space|Zıplama|
|Mouse|Kamera Kontrolü|
|E|Etkileşim (Basma)|
|E (Basılı Tutma)|Sandık Açma (Hold Interaction)|

### Test Senaryoları

1. **Door Test (Toggle):**
    
    - Açık kapıya yaklaşın, "Door" mesajını görün. E'ye basın, kapansın.
        
    - Kapalı kapıya yaklaşın, "Door" mesajını görün. E'ye basın, açılsın.
        
2. **Key + Locked Door Test (Inventory):**
    
    - Kilitli kapıya yaklaşın, "Door is Locked" mesajını görün. E'ye bassanız da açılmamalı.
        
    - Sandıktan çıkan ya da yerde bulduğunuz "Door Key" nesnesini (Instant Interaction) alın.
        
    - Sol üstteki Inventory Debug UI'da anahtarın geldiğini doğrulayın.
        
    - Kilitli kapıya dönün, E'ye basın. Kapı kilidi açılacak ve kapı açılacaktır.
        
3. **Switch Test (Event System):**
    
    - Yerdeki Şaltere (Lever) yaklaşın.
        
    - E'ye basarak kolu indirin.
        
    - Diğer odadaki veya o anki odadaki kapının tetiklendiğini gözlemleyin.
        
4. **Chest Test (Hold Interaction):**
    
    - Sandığa yaklaşın. E tuşuna basmaya başlayınca UI'da bir ilerleme çemberi (Progress Bar) belirecektir.
        
    - E tuşuna 1.5 saniye boyunca **basılı tutun**.
        
    - Bar dolduğunda sandık kapağı açılacak ve içinden item çıkacaktır.
        

---

## Mimari Kararlar

### Interaction System Yapısı

Sistem, **Dependency Inversion** ve **Separation of Concerns** prensipleri gözetilerek 3 ana katmana ayrılmıştır:

1. **Core (Abstraction):** `IInteractable` arayüzü ve `InteractableBase` sınıfı.
    
2. **Logic (Implementation):** `Door`, `Chest`, `Switch`, `ItemPickup` gibi somut sınıflar.
    
3. **Client (Player & UI):** `InteractionDetector` ve `InteractionUI`.
    

**Neden bu yapıyı seçtim:**

> Projenin genişletilebilir olması ve "Spagetti Kod"a dönüşmemesi için UI ve Oyun Mantığını birbirinden tamamen ayırdım (Decoupling). Nesneler kendi durumlarını yönetirken, UI sadece bir "Gözlemci" (Observer) olarak çalışır.

**Alternatifler:**

> UI referansını direkt Interactable scriptlerine vererek (`public InteractionUI ui`) daha hızlı kod yazılabilirdi. Ancak bu, Tight Coupling'e (Sıkı Bağlılık) yol açacağı ve SOLID'e aykırı olduğu için reddettim.

**Trade-off'lar:**

> Event tabanlı yapı kurmak başlangıçta daha fazla kod yazmayı gerektirdi (Boilerplate), ancak hata yönetimi ve modülerlik açısından uzun vadede kazanç sağladı.

### Kullanılan Design Patterns

|**Pattern**|**Kullanım Yeri**|**Neden**|
|---|---|---|
|**Template Method**|`InteractableBase`|Ortak mantığı (Range check, Validation) base class'ta tutup, özel mantığı `OnInteract` abstract metodunda alt sınıflara bırakmak için.|
|**Observer**|`InteractionDetector` Events|UI'ın, etkileşim sistemindeki değişiklikleri (Nesne bulundu, Hold progress vb.) dinlemesi ve kendini güncellemesi için.|
|**Strategy**|`IInteractable`|Farklı etkileşim türlerini (Door, Chest) aynı arayüz üzerinden yönetmek (Polymorphism) için.|

---

## Ludu Arts Standartlarına Uyum

### C# Coding Conventions

|**Kural**|**Uygulandı**|**Notlar**|
|---|---|---|
|m_ prefix (private fields)|[x]|Tüm scriptlerde uygulandı.|
|s_ prefix (private static)|[x]||
|k_ prefix (private const)|[x]||
|Region kullanımı|[x]|Fields, Unity Methods, Public Methods şeklinde ayrıldı.|
|Region sırası doğru|[x]||
|XML documentation|[x]|Tüm public API ve Interface metodlarına eklendi.|
|Silent bypass yok|[x]|Null durumlarında Debug.LogError fırlatılıyor.|
|Explicit interface impl.|[x]|`IInteractable.Interact` explicit olarak uygulandı.|

### Naming Convention

|**Kural**|**Uygulandı**|**Örnekler**|
|---|---|---|
|P_ prefix (Prefab)|[x]|`P_Chest`, `P_Key_01`|
|M_ prefix (Material)|[x]|`M_Chest`|
|T_ prefix (Texture)|[x]|`T_Wood_01`|
|SO isimlendirme|[x]|`DT_DoorKey_01` (Data Template)|

### Prefab Kuralları

|**Kural**|**Uygulandı**|**Notlar**|
|---|---|---|
|Transform (0,0,0)|[x]|Tüm prefablar origin noktasında oluşturuldu.|
|Pivot bottom-center|[x]|Yerleştirme kolaylığı için pivotlar ayarlandı.|
|Collider tercihi|[x]|Performans için Mesh Collider yerine Box Collider kullanıldı.|
|Hierarchy yapısı|[x]|Model ve Logic ayrımı yapıldı.|

### Zorlandığım Noktalar

> Başlangıçta alışkın olduğum "camelCase" değişken isimlendirmesinden, Ludu Arts standardı olan "m_PascalCase" yapısına geçerken kas hafızamı zorladım. Ancak kod tamamlandığında, IDE'de `m_` yazarak tüm local variable'lara erişebilmenin hızını fark ettim.

---

## Tamamlanan Özellikler

### Zorunlu (Must Have)

- [x] Core Interaction System
    
    - [x] IInteractable interface (Explicit implementation)
        
    - [x] InteractionDetector (Raycast based)
        
    - [x] Range kontrolü
        
- [x] Interaction Types
    
    - [x] Instant (Key Pickup)
        
    - [x] Hold (Chest Opening)
        
    - [x] Toggle (Door, Switch)
        
- [x] Interactable Objects
    
    - [x] Door (Locked/Unlocked states)
        
    - [x] Key Pickup (ScriptableObject data)
        
    - [x] Switch/Lever (UnityEvent integration)
        
    - [x] Chest/Container (Item spawning)
        
- [x] UI Feedback
    
    - [x] Interaction prompt (Dynamic text update)
        
    - [x] Dynamic text (Locked vs Open message)
        
    - [x] Hold progress bar (Image fill amount)
        
    - [x] Cannot interact feedback
        
- [x] Simple Inventory
    
    - [x] Key toplama ve ID eşleştirme
        
    - [x] UI listesi (Debug text)
        

### Bonus (Nice to Have)

- [x] Animation entegrasyonu (Kod tabanlı Tween/Coroutine animasyonları)
    
- [x] Chained interactions (Switch ile uzaktaki kapıyı açma)
    
- [ ] Sound effects
    
- [ ] Interaction highlight
    

---

## Bilinen Limitasyonlar

### Tamamlanamayan Özellikler

1. **Save/Load System:** Süre kısıtı ve önceliğin Code Quality'ye verilmesi nedeniyle JSON tabanlı save sistemi eklenmedi.
    

### Bilinen Bug'lar

1. **Physics Interaction:** Sandığın kapağı açılırken fiziksel olarak Player'ı hafifçe itebiliyor (Non-kinematic rigidbody etkileşimi).
    

### İyileştirme Önerileri

1. **Input System:** Proje şu an yarı Legacy yarı New Input System kullanıyor. Tamamen New Input System'e geçilerek Controller desteği eklenebilir.
    
2. **UniTask:** Coroutine'ler yerine UniTask kullanılarak GC (Garbage Collection) yükü azaltılabilir.
    

---

## Ekstra Özellikler

Zorunlu gereksinimlerin dışında eklediklerim:

1. **ScriptableObject Architecture**
    
    - Açıklama: Item'lar (Anahtarlar vb.) ScriptableObject olarak tasarlandı.
        
    - Neden ekledim: Yeni item eklemek için kod yazmaya gerek kalmadan Asset oluşturabilmek için (Data-Driven Design).
        
2. **Event-Driven UI**
    
    - Açıklama: UI scripti Update metodunda çalışmaz, sadece event geldiğinde çalışır.
        
    - Neden ekledim: Performans optimizasyonu ve modülerlik için.
        

---

## Dosya Yapısı

```
Assets/
├── LuduArtsCase/
│   ├── Scripts/
│   │   ├── Runtime/
│   │   │   ├── Core/
│   │   │   │   ├── IInteractable.cs
│   │   │   │   ├── InteractableBase.cs
│   │   │   │   └──InteractionEnums.cs
│   │   │   ├── Interactables/
│   │   │   │   ├── Door.cs
│   │   │   │   ├── Chest.cs
│   │   │   │   ├── Switch.cs
│   │   │   │   └── ItemPickup.cs
│   │   │   ├── Player/
│   │   │   │   ├── InteractionDetector.cs
│   │   │   │   ├── InventoryController.cs
│   │   │   │   └── MovementLogic/
|   │   │   │   │   ├── PlayerLook.cs
|   │   │   │   │   └── PlayerMovement.cs
│   │   │   ├── UI/
│   │   │   |   └── InteractionUI.cs
│   │   │   └── ScriptableObjects/
│   │   │       └── ItemData.cs
│   ├── ScriptableObjects/
│   │   └── Items/
│   ├── Prefabs/
│   ├── Materials/
│   └── Scenes/
│       └── TestScene.unity
├── Docs/
│   ├── CSharp_Coding_Conventions.md
│   ├── Naming_Convention_Kilavuzu.md
│   └── Prefab_Asset_Kurallari.md
├── README.md
├── PROMPTS.md
└── .gitignore
```

---

## İletişim

|**Bilgi**|**Değer**|
|---|---|
|Ad Soyad|Ceyhun Başkoç|
|E-posta|ceyhunbaskoc@gmail.com|
|LinkedIn|https://www.linkedin.com/in/ceyhunbaskoc/|
|GitHub|https://github.com/ceyhunbaskoc|

---

_Bu proje Ludu Arts Unity Developer Intern Case için hazırlanmıştır._