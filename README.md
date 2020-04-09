# UI-Semestralni_projekt
Semestrální projekt do předmětu Umělá inteligence - A-star algoritmus

# Úkol:
Mějme agenta v 2D prostoru, jehož atributem je směr natočení (Sever, Jih, Východ, Západ). Agent může podniknout následující akce:
- Otoč se o 90° doleva (cena 1 bod)
- Otoč se o 90° doprava (cena 1 bod)
- Otoč se o 180° (cena 2 body)
- Udělej krok vpřed o jedno pole (cena 3 body)
Navrhněte v libovolném programovacím jazyku implementaci A* algoritmu tak, aby agent dorazil ze svého počátečního stavu do stavu koncového. Počáteční i koncový stav jsou volitelné. Přípustné akce jsou pouze ty, které agenta umístí na bílé pole.
**********
# Rěšení
Celý program byl zpracován v programovacím jazyce C#. 
- Po převedení bitmapového obrázku do pole nul a jedniček se spustí hlavní metoda “ProjdiOkoliADejMoznyMaticeDoFringe“. V ní se podle směru natočení splní jedna ze čtyř podmínek, a uvnitř ní dochází k:
- Generace polohy pro zjištění ceny
- Určení ceny konkrétního pole
- Zvýšení ceny podle otočení (viz. zadání)
- Generaci plnohodnotné polohy
- Spuštění funkce “ ZkontrolujDuplicityAGenerujPolohy“, která zkontroluje, jestli poloha není duplicitní s některou již prozkoumanou polohou, a pokud není, tak jí přidá do fringe. Další její účel je kontrola, zda jsme již nenašli cílovou lokaci.

*****************************
# Spuštění aplikace:
Windows:
• Je třeba mít soubor UIProjekt1.exe, který je součástí odevzdané práce
• Je třeba mít program, který je schopen ho spustit. V našem případě například IDE Visual studio
• Je třeba mít přiložený soubor MAZE.bmp uložený na disku D ( D:\MAZE.bmp). Pokud by ho chtěl mít uživatel uložen jinde, je třeba upravit cestu ve třídě Bitmapovani na 23. řádku.
