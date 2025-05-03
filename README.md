# Ricochet-Robot-powers

Une version personnellement codée du jeu de table *Ricochet Robots*, avec des paramètres optionnels permettant aux robots d’avoir des **pouvoirs spéciaux**, accessibles depuis le menu **"cursed settings"**.

## 🔧 Pouvoirs des robots (optionnels)

- **🔵 Bleu** : peut se **téléporter de 4 cases orthogonalement** une fois par round.  
- **⚪ Gris** : **passe à travers tous les murs**. Notez que ce pouvoir est **permanent** lors de la partie.  
- **🟡 Jaune** : peut **traverser un mur** par round.  
- **🟢 Vert** : peut faire apparaître, **une fois par tour**, un **hologramme** dans n’importe quelle direction orthogonale, tant qu’il n’y a pas d’obstacle entre lui et l’emplacement choisi.  
  L’hologramme disparaît si Vert bouge ou si le pouvoir est désactivé.  
- **🔴 Rouge** : peut **agripper ou désagripper** tout robot adjacent. Les **pouvoirs actifs des robots agrippés s’appliquent également à tous les robots qui sont agrippés**.

---

## 🧠 Présentation du jeu

*Ricochet Robot* est un jeu de réflexion dans lequel les joueurs doivent trouver le chemin le plus court qu’un robot peut emprunter pour atteindre une cible, en se déplaçant uniquement dans les **directions orthogonales** (haut, bas, gauche, droite) et en s’arrêtant uniquement lorsqu’il rencontre un **obstacle** (mur ou autre robot).  
L’objectif est de réaliser ce parcours avec **le moins de mouvements possibles**.

### 🎯 But du jeu

Amener le robot de la couleur de l’objectif central sur la case où se trouve le **symbole correspondant**.  
Les joueurs réfléchissent simultanément sans toucher aux robots.

### 🚗 Déplacements

- Un robot se déplace en **ligne droite** jusqu’à rencontrer un **mur** ou un **autre robot**.  
- Il est possible d’utiliser les autres robots pour créer des obstacles.  
- Un déplacement vers un obstacle compte pour **1 coup**.  
- Lorsqu’un robot rencontre un **mur de couleur**, il le traverse uniquement s’il correspond à sa propre couleur ; sinon, il **rebondit à 90°** (ce rebond ne compte pas comme un mouvement).

### ⏱️ Tour de jeu

- Dès qu’un joueur pense avoir trouvé la meilleure solution, il **dit le nombre de mouvement** et **démarre le minuteur**.  
- Les autres ont **une minute** pour tenter de trouver une meilleure solution.  
- On vérifie ensuite les propositions : si la meilleure est correcte, le joueur marque le point.

### 🏁 Fin de partie

Le joueur ayant cumulé **le plus de points** gagne la partie.

---

## 🕹️ Fonctionnement du jeu (version codée)

- Pour **déplacer un robot** :  
  Maintenez la **touche de direction** souhaitée et **cliquez gauche** sur le robot.

- Pour **activer un pouvoir**, appuyez sur le **bouton correspondant**, puis suivez cette légende :  
  - **🔵 Bleu, 🟡 Jaune** : bouton → déplacer le robot.  
  - **🔴 Rouge** : bouton → cliquez sur un robot adjacent pour l’agripper ou le désagripper.  
  - **🟢 Vert** : bouton → cliquez sur Vert pour faire apparaître ou disparaître un hologramme.
---

