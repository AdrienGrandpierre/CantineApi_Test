# Client Service API

- [Technologies utilisées](#technologies-utilisées)
- [Structure des contrôleurs](#structure-des-contrôleurs)
  - [ClientController](#clientcontroller)
  - [PlateauController](#plateaucontroller)
- [Exemple de requêtes](#exemple-de-requêtes)

---

## Technologies utilisées

- **.NET 8**
- **C#**
- **ASP.NET Core** pour la gestion des contrôleurs et des endpoints API.
- Swagger pour la documentation interactive de l'API.

---

## Structure des contrôleurs

### ClientController

Le contrôleur `ClientController` gère les opérations liées aux clients.

 endpoints :
- `GET /api/client/{id}` : Récupère un client par son identifiant.
- `POST /api/client/{id}/credit` : Crédite un montant sur le compte d'un client.
- `GET /api/client` : Récupère la liste complète des clients.

### PlateauController

Le contrôleur `PlateauController` gère la création des plateaux.

endpoints :
- `POST /api/plateau/create-plateau` : Crée un plateau à partir des données envoyées (identifiant client et produits).

---

## Exemple de requêtes

### Récupérer un client par ID

**Requête** :
```http
GET /api/client/1 HTTP/1.1
Host: localhost:5000
```

**Réponse** :
```json
{
    "id": 1,
    "name": "Alice",
    "type": "Interne",
    "credit": 20
}
```

### Créditer un client

**Requête** :
```http
POST /api/client/1/credit HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
    "amount": 50
}
```

**Réponse** :
```http
204 No Content
```

### Créer un plateau

**Requête** :
```http
POST /api/plateau/create-plateau HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
    "clientId": 1,
    "produitRequests": [
        { "name": "entrée", "type": "entrée" },
        { "name": "plat", "type": "plat" },
        { "name": "dessert", "type": "dessert" },
        { "name": "pain", "type": "pain" }
    ]
}
```

**Réponse** :
```json
{
    "produits": ["entrée", "plat", "dessert", "pain"],
    "total": 4,
    "priseEnCharge": "6"
}
```

