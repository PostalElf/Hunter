Module Module1
    Sub Main()
        Dim battlefield As New Battlefield
        battlefield.Add(BuildMech)
        battlefield.Add(BuildMonster)
        battlefield.Main()
    End Sub
    Private Function BuildMech() As CombatantPlayer
        Dim bodyparts As New List(Of Bodypart)

        Dim baseBodypart As Bodypart = Bodypart.Construct(New List(Of String) From { _
            "Weight: 50", _
            "Carry: 75", _
            "Speed: 10", _
            "ShockCapacity: 100" _
        })
        bodyparts.Add(Bodypart.Construct(New List(Of String) From { _
            "Name: Left Arm", _
            "Weight: 10", _
            "Armour: 5", _
            "Health: 10", _
            "ShockLoss: 10", _
            New Attack("Powerfist", 1, 1, 3, 95, 40, DamageType.Kinetic, 5, 2).Export _
        }))
        bodyparts.Add(Bodypart.Construct(New List(Of String) From { _
            "Name: Left Arm", _
            "Weight: 10", _
            "Armour: 5", _
            "Health: 10", _
            "ShockLoss: 10", _
            New Attack("Powerfist", 1, 1, 3, 95, 40, DamageType.Kinetic, 5, 2).Export _
        }))
        bodyparts.Add(Bodypart.Construct(New List(Of String) From { _
            "Name: LRM Launcher", _
            "Weight: 5", _
            "Armour: 5", _
            "Health: 10", _
            "ShockLoss: 5", _
            New Attack("LRM", 3, 2, 2, 45, -10, DamageType.Explosive, 3, 3).Export _
        }))
        Return Combatant.Construct("p"c, "Fenris", baseBodypart, bodyparts)
    End Function
    Private Function BuildMonster() As CombatantAI
        Dim bodyparts As New List(Of Bodypart)
        Dim baseBodypart As Bodypart = Bodypart.Construct(New List(Of String) From { _
            "Speed: 25", _
            "ShockCapacity: 100" _
        })
        bodyparts.Add(Bodypart.Construct(New List(Of String) From { _
            "Name: Arms", _
            "Armour: 0", _
            "Health: 25", _
            "ShockLoss: 50", _
            New Attack("Claws", 0, 3, 1, 65, 25, DamageType.Kinetic, 4, 1).Export _
        }))
        Return Combatant.Construct("a"c, "Goblin", baseBodypart, bodyparts)
    End Function
End Module
