# 🎉 Gestion des Invitations aux Événements

Une application web permettant aux organisateurs de créer des événements, envoyer des invitations aux visiteurs avec vérification par email via Deepseek API, et générer des badges QR pour les participants acceptés.

---

## 🛠️ Technologies utilisées

- **Backend** : ASP.NET Core (.NET 8)
- **Frontend** : Angular 18, Tailwind Css
- **Base de données** : SQL Server
- **Conteneurisation** : Docker, Docker Compose
- **Email/Génération** : Deepseek API

---

## 📌 Fonctionnalités principales

- Création de compte organisateur
- Création et gestion des événements
- Envoi automatique d’e-mails de vérification (Deepseek)
- Validation ou refus des inscriptions
- Génération de badge QR pour les visiteurs validés
- Interface utilisateur responsive et intuitive
- Conteneurisation de l’ensemble du projet avec Docker

---

## 🧩 Architecture du Projet
<img src="https://github.com/user-attachments/assets/00a03535-5f16-4644-a0ac-8818a0500ed7" width="700"/>
<img src="https://github.com/user-attachments/assets/3e7ca9cb-7bff-4e7d-8729-c5fc2becc87c" width="700"/>



## 💻 Installation


```bash
# Clone repository
git clone git@github.com:yassineouabou/Gestion-des-invitations.git

#Crée un fichier nommé appsettings.Development.Local.json dans le dossier api/
"FromEmail": "votre email",
"FromPassword": "",
"AI_ApiUrl": "URL",
"AI_ApiKey": "Key"

#Installez Docker Desktop et lancer

# Ouvrir le backend dans Visual Studio et dans le terminal Exécuter les conteneurs Docker
docker-compose up --build
```

