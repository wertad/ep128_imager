# ep128_imager
Windowsos segédprogram a retropie-os ep128 emulator programok 800KB-os floppy lemezképfájlra való kiírásához.

- a programnak első indítást követően meg kell adni egy elérési utat, hogy honnan keresse a beérkező .rar fájlokat, valamint kiválasztani egy már csatolt 800KB-os virtuális drive-ot (csak byte-ra pontosan 809984 méretű 
  meghajtót listáz és kezel, ugyanis törlési folyamat is van beleépítve, így ez egy óvintézkedés is a balesetek elkerülése érdekében)
- ha nem listáz floppy drive-ot, akkor fel kell csatolni egyet pl. ImDisk-kel és újraindítani az Ep128 Imager-t.
- ezek után amíg fut a program figyeli a megadott watchfoldert, ha abba .rar fájl kerül azt feldolgozza:
	1. kitömríti a _watchfolder_\PROGRAM_NEVE\src\ folderbe
	2. letörli a .rar-t
	3. kitörli a megadott lemezmeghajtóról az összes fájlt kivéve az EXDOS.INI-t
	4. átírja az EXDOS.INI-ben a megfelelő .COM fájlra a parancsot
	5. lementi _watchfolder_\PROGRAM_NEVE\PROGRAM_NEVE.img fájlba a programot 
- fontos tudni, hogyha hálózati meghajtót adsz meg mapped drive-val betűjelhez rendelve watchfolder-nek (pl. 'W:\' ,ami egy hálózati megosztásra van csatolva), akkor bizonyos windows verzióknál gondok lehetnek.
  Ennek eredményeképpen hiába tömöríti ki és készít elő mindent a virtuális drive-ra a program, az .img fájl nem mentődik le. Erra a megoldást egy html fájlba mellékeltem
 
![ep128](https://user-images.githubusercontent.com/17532282/194718804-86c6974b-0737-4ee6-9eac-0ae9487bf892.png)
