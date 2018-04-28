Public MustInherit Class Combatant
    Public Shared Function Construct(ByVal combatantType As Char, ByVal _name As String, ByVal _baseBodypart As Bodypart, ByVal _bodyparts As List(Of Bodypart)) As Combatant
        Dim c
        If combatantType = "p" Then
            c = New CombatantPlayer
        ElseIf combatantType = "a" Then
            c = New CombatantAI
        Else
            Throw New Exception("Invalid combatantType: " & combatantType)
        End If
        With c
            .Name = _name
            .BaseBodypart = _baseBodypart
            .Bodyparts = _bodyparts
        End With
        Return c
    End Function

#Region "Personal Identifiers"
    Protected Name As String
#End Region

#Region "Bodyparts"
    Protected Bodyparts As New List(Of Bodypart)
    Protected BaseBodypart As Bodypart
    Public Sub Remove(ByVal bp As Bodypart)
        If Bodyparts.Contains(bp) = False Then Exit Sub
        Bodyparts.Remove(bp)
    End Sub
    Public Function GetBodypartsTargetable() As List(Of Bodypart)
        Return Bodyparts
    End Function
#End Region

#Region "Properties"
    Public ReadOnly Property Weight As Integer
        Get
            Dim total As Integer = BaseBodypart.BonusWeight
            For Each bp In Bodyparts
                total += bp.BonusWeight
            Next
            Return total
        End Get
    End Property
    Public ReadOnly Property Carry As Integer
        Get
            Dim total As Integer = BaseBodypart.BonusCarry
            For Each bp In Bodyparts
                total += bp.BonusCarry
            Next
            Return total
        End Get
    End Property
    Public ReadOnly Property Encumbrance As Integer
        Get
            Return Math.Ceiling((Weight / Carry) * 100)
        End Get
    End Property
    Public ReadOnly Property EncumbranceModifier As Double
        Get
            Select Case Encumbrance
                Case Is <= 50 : Return 1
                Case 51 To 70 : Return 0.75
                Case 71 To 85 : Return 0.5
                Case Else : Return 0.25
            End Select
        End Get
    End Property
    Public ReadOnly Property Speed As Integer
        Get
            Dim total As Integer = BaseBodypart.BonusSpeed
            For Each bp In Bodyparts
                total += bp.BonusSpeed
            Next
            Return Math.Ceiling(total * EncumbranceModifier)
        End Get
    End Property
    Public ReadOnly Property Dodge As Integer
        Get
            Dim total As Integer = BaseBodypart.BonusDodge
            For Each bp In Bodyparts
                total += bp.BonusDodge
            Next
            Return Math.Ceiling(total * EncumbranceModifier)
        End Get
    End Property
    Public ReadOnly Property ShockCapacity As Integer
        Get
            Dim total As Integer = BaseBodypart.BonusShockCapacity
            For Each bp In Bodyparts
                total += bp.BonusShockCapacity
            Next
            Return total
        End Get
    End Property
#End Region

#Region "Combat"
    Private Battlefield As Battlefield
    Private Position As BattlefieldPosition
    Public Function GetDistance(ByVal target As Combatant) As Integer
        Return Position + target.Position
    End Function

    Private Initiative As Integer
    Public Function Tick(ByVal battlefield As Battlefield) As Boolean
        Const MinInitiative As Integer = 3
        Initiative -= 1
        If Initiative = 0 Then
            Initiative = Battlefield.HighestSpeed - Speed
            If Initiative < MinInitiative Then Initiative = MinInitiative
            PerformTurn(battlefield)
            Return True
        Else
            Return False
        End If
    End Function
    MustOverride Sub PerformTurn(ByVal battlefield As Battlefield)

    Public ReadOnly Property AttacksAll As List(Of Attack)
        Get
            Dim total As New List(Of Attack)
            If BaseBodypart.Attack.Name <> "" Then total.Add(BaseBodypart.Attack)
            For Each bp In Bodyparts
                If bp.Name <> "" Then total.Add(bp.Attack)
            Next
            Return total
        End Get
    End Property
    Public ReadOnly Property AttacksReady As List(Of Attack)
        Get
            Dim total As New List(Of Attack)
            If BaseBodypart.Attack.Name <> "" Then total.Add(BaseBodypart.Attack)
            For Each bp In Bodyparts
                If bp.AttackReady = True Then total.Add(bp.Attack)
            Next
            Return total
        End Get
    End Property

    Private ShockTaken As Integer
    Public Sub AddShock(ByVal value As Integer)
        Console.WriteLine(Name & " suffers " & value & " Shock!")
        ShockTaken += value
        If ShockTaken >= ShockCapacity Then Destroyed()
    End Sub
    Private Sub Destroyed()
        Console.WriteLine(Name & " has been destroyed!!!")
        Battlefield.Remove(Me)
    End Sub
#End Region
End Class
