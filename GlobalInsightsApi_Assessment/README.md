# GlobalInsightsApi_Assessment

## Περιγραφή (Overview)

Το project αυτό υλοποιεί ένα API aggregation service (χρησιμοποιώντας ASP.NET Core) το οποίο συγκεντρώνει δεδομένα από τουλάχιστον τρία εξωτερικά API (π.χ. OpenWeatherMap, News API, GitHub API) και παρέχει ένα ενιαίο endpoint (π.χ. /api/insights/aggregate) για την παράλληλη ανάκτηση και φιλτράρισμα/ταξινόμηση των δεδομένων. Το project σχεδιάστηκε με έμφαση στην ευελιξία (modular design), την ασύγχρονη επεξεργασία (παράλληλη ανάκτηση) και την αξιοπιστία (π.χ. fallback σε περίπτωση αποτυχίας ενός API).

## Οδηγίες Εγκατάστασης & Ρύθμισης (Installation & Setup)

1. Κλωνοποιήστε το repository (π.χ. με git clone).
2. Ανοίξτε το project (π.χ. με Visual Studio ή dotnet CLI) και βεβαιωθείτε ότι έχετε εγκατεστημένο το .NET SDK (π.χ. .NET 7 ή νεότερη έκδοση).
3. Ρυθμίστε τα API κλειδιά (π.χ. για OpenWeatherMap, News API, GitHub API) στο αρχείο ρυθμίσεων (π.χ. appsettings.json). Παράδειγμα (προαιρετικό):

   ```json
   {
     "OpenWeatherSettings": { "ApiKey": "your_openweather_api_key" },
     "NewsSettings": { "ApiKey": "your_news_api_key" },
     "GitHubSettings": { "ApiKey": "your_github_api_key" }
   }
   ```
4. Εκτελέστε (π.χ. dotnet build) και τρέξτε το project (π.χ. dotnet run).

## API Endpoints

### 1. Aggregation Endpoint (GET /api/insights/aggregate)

Συγκεντρώνει δεδομένα από όλα τα εξωτερικά API (παράλληλα) και επιστρέφει ένα ενιαίο JSON αντικείμενο (AggregatedInsights).

**Query Parameters (Required):**

• city (π.χ. "Athens") – η πόλη για τα δεδομένα καιρού.  
• query (π.χ. "news") – το query για τα δεδομένα ειδήσεων.  
• username (π.χ. "testuser") – το username για τα δεδομένα του GitHub.

**Προαιρετικά Query Parameters (π.χ. για φιλτράρισμα/ταξινόμηση):**

• date (π.χ. "2023-01-01") – (προαιρετικό) φιλτράρισμα με ημερομηνία.  
• category (π.χ. "sports") – (προαιρετικό) φιλτράρισμα με κατηγορία.  
• sort (π.χ. "asc") – (προαιρετικό) ταξινόμηση (π.χ. ανά ημερομηνία).

**Παράδειγμα Αίτησης (Request):**

GET /api/insights/aggregate?city=Athens&query=news&username=testuser

**Παράδειγμα Απάντησης (Response) (JSON):**

```json
{
  "weather": { "city": "Athens", "temperature": 25, "humidity": 60, "wind_speed": 5, "feels_like": 26, "description": "sunny" },
  "news": [ { "title": "News Title", "description": "News Description", "url": "https://example.com/news", "source": "News Source", "publishedAt": "2023-01-01T00:00:00Z" } ],
  "github": { "login": "testuser", "id": 123, "avatar_url": "https://example.com/avatar.png", "html_url": "https://github.com/testuser", "public_repos": 10, "followers": 5, "following": 3, "created_at": "2020-01-01T00:00:00Z", "updated_at": "2023-01-01T00:00:00Z" }
}
```

### 2. (Προαιρετικά) Endpoints για Ατομικά API Δεδομένα

#### (a) Weather Endpoint (GET /api/insights/weather)

Επιστρέφει δεδομένα καιρού για την πόλη που δίνεται (π.χ. /api/insights/weather?city=Athens).

**Query Parameter (Required):**

• city – η πόλη (π.χ. "Athens").

**Παράδειγμα Απάντησης (JSON):**

```json
{
  "city": "Athens",
  "temperature": 25,
  "humidity": 60,
  "wind_speed": 5,
  "feels_like": 26,
  "description": "sunny"
}
```

#### (b) News Endpoint (GET /api/insights/news)

Επιστρέφει δεδομένα ειδήσεων με βάση το query (π.χ. /api/insights/news?query=news).

**Query Parameter (Required):**

• query – το query (π.χ. "news").

**Παράδειγμα Απάντησης (JSON):**

```json
[
  { "title": "News Title", "description": "News Description", "url": "https://example.com/news", "source": "News Source", "publishedAt": "2023-01-01T00:00:00Z" }
]
```

#### (c) GitHub Endpoint (GET /api/insights/github)

Επιστρέφει δεδομένα του GitHub για το username που δίνεται (π.χ. /api/insights/github?username=testuser).

**Query Parameter (Required):**

• username – το username (π.χ. "testuser").

**Παράδειγμα Απάντησης (JSON):**

```json
{
  "login": "testuser",
  "id": 123,
  "avatar_url": "https://example.com/avatar.png",
  "html_url": "https://github.com/testuser",
  "public_repos": 10,
  "followers": 5,
  "following": 3,
  "created_at": "2020-01-01T00:00:00Z",
  "updated_at": "2023-01-01T00:00:00Z"
}
```

## Αρχιτεκτονική (Architecture)

• **Modular Design:** Το project χρησιμοποιεί ένα modular σχεδιασμό (π.χ. χωρισμός σε Services, Clients, Controllers) ώστε να είναι εύκολο η προσθήκη νέων εξωτερικών API.  
• **Ασύγχρονη Επεξεργασία (Asynchronous Processing):** Τα δεδομένα από τα εξωτερικά API αντανακτώνται παράλληλα (π.χ. με Task.WhenAll) για βελτιωμένη απόδοση.  
• **Error Handling:** Υλοποιείται fallback (π.χ. επιστροφή μερικών δεδομένων αν ένα API αποτύχει) και επιστρέφονται κατάλληλα HTTP status codes (π.χ. 400 αν λείπει παράμετρος, 404 αν δεν υπάρχουν δεδομένα).

## Unit Tests

Τα unit tests (για παράδειγμα, για τον InsightsController και το InsightsService) υλοποιούνται με χρήση του xUnit, του Moq (για mock) και του FluentAssertions. Για να τρέξετε τα tests, εκτελέστε (π.χ. με dotnet test) την εντολή:

   dotnet test

## (Προαιρετικά) Caching και Στατιστικά Στοιχεία (Caching & Statistics)

• **Caching:** (Προαιρετικά) μπορείτε να υλοποιήσετε caching (π.χ. με IMemoryCache ή IDistributedCache) για να αποθηκεύετε (προσωρινά) τις απαντήσεις των εξωτερικών API και να μειώσετε τα επαναλαμβανόμενα API calls.  
• **Στατιστικά Στοιχεία (Statistics Endpoint):** (Προαιρετικά) μπορείτε να προσθέσετε ένα endpoint (π.χ. GET /api/insights/statistics) που επιστρέφει στατιστικά στοιχεία (π.χ. αριθμό αιτημάτων ανά API, χρόνο απόκρισης, rate limits).

---
