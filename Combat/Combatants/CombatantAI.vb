Public Class CombatantAI
    Inherits Combatant
    Public Overrides Sub PerformTurn(ByVal battlefield As Battlefield)
        'tick
        For Each bp In Bodyparts
            bp.Tick()
        Next

        If CurrentAttack.Name = "" Then CurrentAttack = GetAttackWithLowestCooldown()

        If CurrentTarget Is Nothing Then
            Dim targets As List(Of Combatant) = battlefield.GetTargets(Me, CurrentAttack)
            If targets.Count = 0 Then
                'move to optimal range
                Dim range As Integer = CurrentAttack.Range

            Else
                Dim target As Combatant = GetRandom(Of Combatant)(targets)
                CurrentTarget = target
            End If
        End If

        'get random bodypart and attack
        Dim bodypart As Bodypart = GetRandom(Of Bodypart)(CurrentTarget.GetBodypartsTargetable)
        bodypart.Attacked(CurrentAttack)
    End Sub

    Private CurrentAttack As Attack
    Private CurrentTarget As Combatant
    Private Function GetAttackWithLowestCooldown() As Attack

    End Function
End Class
