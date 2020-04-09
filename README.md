# UI-Semestralni_projekt
Semestrální projekt do předmětu Umělá inteligence - A-star algoritmus

# Úkol:
Mějme agenta v 2D prostoru, jehož atributem je směr natočení (Sever, Jih, Východ, Západ). Agent může podniknout následující akce:
- Otoč se o 90° doleva (cena 1 bod)
- Otoč se o 90° doprava (cena 1 bod)
- Otoč se o 180° (cena 2 body)
- Udělej krok vpřed o jedno pole (cena 3 body)
Navrhněte v libovolném programovacím jazyku implementaci A* algoritmu tak, aby agent dorazil ze svého počátečního stavu do stavu koncového. Počáteční i koncový stav jsou volitelné. Přípustné akce jsou pouze ty, které agenta umístí na bílé pole.

R23en9
# UI-Semestralni_projekt_2
Celý program byl zpracován v programovacím jazyce C#. 
- Úkolem projektu bylo najít nejkratší cestu mezi městy v Tasmánii s využitím logiky genetického algoritmu. Hodnoty vzdáleností mezi nimi jsou v dokumentu Distance.xlsx. 
********
# Postup
Jednotlivé kroky genetického algoritmu v našem případě jsou:
- Inicializace – vytvoření první generace
- Vyhodnocení – určení celkové vzdálenosti pro jednotlivé trasy
- Selekce – kratší trasy mají větší šanci na zachování
- Křížení – promísení částí dvou tras
- Mutace – náhodné změny
- Opakování – proces se opakuje od bodu vyhodnocení
*****************************
