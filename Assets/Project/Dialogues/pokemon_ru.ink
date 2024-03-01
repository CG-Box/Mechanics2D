INCLUDE globals.ink

{ pokemon_name == "": -> main | -> already_chose }

=== main ===
//Which pokemon do you choose? 
Какого покемона ты выбираешь?
    + [Чармандер]
        -> chosen("Чармандер")
    + [Бульбазавр]
        -> chosen("Бульбазавр")
    + [Сквиртл]
        -> chosen("Сквиртл")
        
=== chosen(pokemon) ===
~ pokemon_name = pokemon
~Log(pokemon_name)
//You chose {pokemon}!
Вы выбрали {pokemon}!
-> END

=== already_chose ===
//You already chose {pokemon_name}!
Вы уже выбрали {pokemon_name}!
-> END