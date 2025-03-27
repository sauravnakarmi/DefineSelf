INCLUDE globals.ink

{ pokemonName == "" : -> main | -> alreadyChose }

-> main

=== main ===
Which pokemon do you choose? #speaker:Mr. Blue #portrait:mr_blue_neutral #layout:right #audio:animal_crossing_low
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulbasaur")
    + [Squirtle]
        -> chosen("Squirtle")
        
=== chosen(pokemon) ===
~ pokemonName = pokemon
You chose {pokemon}!
-> END

=== alreadyChose ===
You already chose {pokemonName} #audio:animal_crossing_low
-> END