VAR catsaved = 0

-> main
=== main ===
< There is a cat stuck on the tree >
Meow! < The cat looks scared >
    * [Meow Back] -> Meow
    * [Ignore it] -> Ignore
=== Meow ===
< The cat jumps down from the tree >
~ catsaved = 1
-> END
== Ignore ==
< What kind of monster ignore a cat? >
-> END





