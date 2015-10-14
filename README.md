# Term3D
GitHub repository of the application Term3D

Coucou les gens !

Si vous voulez un petit tuto sur comment faire des bails avec le git, vous êtes au bon endroit !

## Utiliser git sur votre branche

Hum, alors, tout simplement, quand vous bossez, vous bossez sur votre branche (wajisan pour Wadi, shirokai pour Adrien, etc).

Avant de bosser, faites un
```bash
git pull origin {branche_au_dessus}
````
La {branche_au_dessus} réfère à la branche network pour ceux qui travaillent sur la gestion du réseau, la branche graphic pour ceux qui travaillent sur le graphique et la branche core pour ceux qui travaillent sur le core.

Une fois qu'vous avez fini de bosser, vous faites un
```bash
git commit -am {petit message explicatif}
````
Par pitié, prenez le temps de faire le fameux {petit message explicatif}.
Ensuite
```bash
git pull origin {branche_au_dessus}
````
Et oui, on pull encore ! Pourquoi ? Eh bien, tout simplement pour que vous ne ratiez pas une update qui aurait été faite sur la {branche_au_dessus} pendant que vous bossiez.
Là, git devrait merge vos bails. Si y'a des conflits, réglez-les. Puis
```bash
git push
````
Et voilà, vos changements sont répercutés sur votre branche !
