# Term3D
GitHub repository of the application Term3D

Coucou les gens !

Si vous voulez un petit tuto sur comment faire des bails avec le git, vous êtes au bon endroit !

## Utiliser git sur votre branche

Hum, alors, tout simplement, quand vous bossez, vous bossez sur la branche de votre partie (core, graphic ou netwwork).

Pour avoir la branche en local (faites ça que la première fois) :
```bash
git clone -b {branche} https://github.com/Thuwa77/Term3D.git
````

Avant de bosser, faites un
```bash
git pull origin {branche}
````

Une fois qu'vous avez fini de bosser, vous faites un
```bash
git commit -am {petit message explicatif}
````
Par pitié, prenez le temps de faire le fameux {petit message explicatif}.

Ensuite
```bash
git pull origin {branche}
````
Et oui, on pull encore ! Pourquoi ? Eh bien, tout simplement pour que vous ne ratiez pas une update qui aurait été faite sur la {branche} pendant que vous bossiez.

Là, git devrait merge vos bails. Si y'a des conflits, réglez-les. Puis
```bash
git push
````
Et voilà, vos changements sont répercutés sur votre branche !

## Répercuter les changements sur preprod

Alors, c'est bien beau de travailler sur votre partie, mais faudra merge un jour !
Pour merge la preprod, il suffit de :
- Sauter sur la preprod
- git pull {branche}
- Si y'a des problèmes de merge, règlez-les puis faites git commit -am "merge preprod and {branche}"
- git push
- ???
- PROFIT!!!
