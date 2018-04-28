Public Class Battlefield
    Private Attackers As New List(Of Combatant)
    Private Defenders As New List(Of Combatant)
    Public Sub Add(ByVal c As Combatant)
        If TypeOf c Is CombatantPlayer Then Attackers.Add(c)
        If TypeOf c Is CombatantAI Then Defenders.Add(c)
    End Sub
    Public Sub Remove(ByVal c As Combatant)
        If Attackers.Contains(c) = True Then
            Attackers.Remove(c)
        ElseIf Defenders.Contains(c) = True Then
            Defenders.Remove(c)
        End If
    End Sub

    Public ReadOnly Property HighestSpeed As Integer
        Get
            Dim highest As Integer = -1
            For Each a In Attackers
                If a.Speed > highest Then highest = a.Speed
            Next
            For Each d In Defenders
                If d.Speed > highest Then highest = d.Speed
            Next
            Return highest
        End Get
    End Property
    Public Function GetTargets(ByVal attacker As Combatant, ByVal attack As Attack) As List(Of Combatant)
        Dim possibleTargets As List(Of Combatant)
        If TypeOf attacker Is CombatantPlayer Then
            possibleTargets = Defenders
        ElseIf TypeOf attacker Is CombatantAI Then
            possibleTargets = Attackers
        Else
            Throw New Exception("Invalid attacker type")
        End If

        For n = possibleTargets.Count - 1 To 0 Step -1
            Dim target As Combatant = possibleTargets(n)
            If attack.Range < attacker.GetDistance(target) Then possibleTargets.Remove(target) : Continue For
        Next

        Return possibleTargets
    End Function

    Public Sub Main()
        'hasActed keeps track of whether a combatant (any combatant) has acted
        'if hasActed = false, skip checking whether a side wins
        Dim hasActed As Boolean = False

        While True
            'check to see if one side wins
            If hasActed = True Then
                hasActed = False
                If Attackers.Count = 0 Then
                    AttackersWin()
                    Exit While
                ElseIf Defenders.Count = 0 Then
                    DefendersWin()
                    Exit While
                End If
            End If

            'tick each combatant
            For Each c In Attackers
                If c.Tick(Me) = True Then hasActed = True
            Next
            For Each c In Defenders
                If c.Tick(Me) = True Then hasActed = True
            Next
        End While
    End Sub
    Private Sub AttackersWin()
        Console.WriteLine("Attackers win!")
        Console.ReadLine()
    End Sub
    Private Sub DefendersWin()
        Console.WriteLine("Defenders win!")
        Console.ReadLine()
    End Sub
End Class
