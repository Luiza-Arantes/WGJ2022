-> main

=== main ===
Oi, eu sou um texto

Which pokemon do you choose?
    * [Charmander]
        -> chosen("Charmander")
    * [Bulbasaur]
        -> chosen("Bulbasaur")
    * [Squirtle]
        -> chosen("Squirtle")
    * -> afterAllFailedChoices

-> DONE

=== chosen(pokemon) ===
You chose {pokemon}!

Parece que não é a resposta certa, vamos tentar de novo?
-> main

-> DONE

=== afterAllFailedChoices ===
Hm, você está tentando coisas muito específicas, vamos tentar essas daqui?

    * [Escolha 1]
        A escolha 1 era exatamente o que eu queria!
        -> DONE
        
    * Escolha 2
        Arrasou!
        -> DONE
        
Tchau

-> END



