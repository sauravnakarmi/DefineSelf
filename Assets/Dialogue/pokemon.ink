INCLUDE globals.ink

{ pokemonName == "" : -> main | -> alreadyChose }

-> main

=== main ===
Which pokemon do you choose?
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
You already chose {pokemonName}
-> END