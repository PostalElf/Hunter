Public Class Bodypart
    Public Shared Function Construct(ByVal rawdata As List(Of String)) As Bodypart
        Dim bp As New Bodypart
        For Each ln In rawdata
            Dim l As String() = ln.Split(":")
            Dim header As String = l(0).Trim
            Dim entry As String = l(1).Trim
            bp.Construct(header, entry)
        Next
        Return bp
    End Function
    Private Sub Construct(ByVal header As String, ByVal entry As String)
        Select Case header
            Case "Name" : _Name = entry
            Case "Weight" : _BonusWeight = CInt(entry)
            Case "Carry" : _BonusCarry = CInt(entry)
            Case "Speed" : _BonusSpeed = CInt(entry)
            Case "Dodge" : _BonusDodge = CInt(entry)
            Case "ShockCapacity" : _BonusShockCapacity = CInt(entry)

            Case "Agility" : Agility = CInt(entry)
            Case "Armour" : Armour = CInt(entry)
            Case "Health" : Health = CInt(entry)
            Case "ShockAbsorb" : ShockAbsorb = CDbl(entry)
            Case "ShockLoss" : ShockLoss = CInt(entry)
            Case "Attack" : _Attack = New Attack(entry)
        End Select
    End Sub

#Region "Personal Identifiers"
    Private _Name As String
    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property
    Private _Owner As Combatant
    Public WriteOnly Property Owner As Combatant
        Set(ByVal value As Combatant)
            _Owner = value
        End Set
    End Property
#End Region

#Region "Bonuses"
    Private _BonusWeight As Integer
    Public ReadOnly Property BonusWeight As Integer
        Get
            Return _BonusWeight
        End Get
    End Property
    Private _BonusCarry As Integer
    Public ReadOnly Property BonusCarry As Integer
        Get
            Return _BonusCarry
        End Get
    End Property
    Private _BonusSpeed As Integer
    Public ReadOnly Property BonusSpeed As Integer
        Get
            Return _BonusSpeed
        End Get
    End Property
    Private _BonusDodge As Integer
    Public ReadOnly Property BonusDodge As Integer
        Get
            Return _BonusDodge
        End Get
    End Property
    Private _BonusShockCapacity As Integer
    Public ReadOnly Property BonusShockCapacity As Integer
        Get
            Return _BonusShockCapacity
        End Get
    End Property
#End Region

#Region "Bodypart specific properties"
    Private Agility As Integer
    Private Armour As Integer
    Private Health As Integer
    Private ShockAbsorb As Double
    Private ShockLoss As Integer

    Private _Attack As Attack
    Public ReadOnly Property Attack As Attack
        Get
            Return _Attack
        End Get
    End Property
    Private AttackCooldown As Integer
    Public ReadOnly Property AttackReady As Boolean
        Get
            If _Attack.Name = "" Then Return False
            If AttackCooldown > 0 Then Return False
            Return True
        End Get
    End Property

    Public Sub Tick()
        If AttackCooldown > 0 Then AttackCooldown -= 1
    End Sub
#End Region

#Region "Damage"
    Private DamageTaken As Integer
    Public Sub Attacked(ByVal attack As Attack)
        Dim trueDodge As Integer = _Owner.Dodge + Agility
        Dim roll As Integer = rng.Next(1, 101)
        If roll <= attack.Accuracy - trueDodge Then
            roll = rng.Next(1, 101)
            If roll <= attack.Penetration - Armour Then
                AddDamage(attack.DamageFull, attack.DamageType)
            Else
                AddDamage(attack.DamageGlancing, attack.DamageType)
            End If
        Else
            AttackMiss(attack)
        End If
    End Sub
    Private Sub AttackMiss(ByVal attack As Attack)
        Console.WriteLine(Name & " dodged the attack!")
    End Sub
    Private Sub AddDamage(ByVal value As Integer, ByVal type As DamageType)
        'add damage to bodypart
        DamageTaken += value
        Console.WriteLine(Name & " is hit for " & value & " " & type.ToString & ".")

        'add shock from damage
        Dim shock As Integer = Math.Ceiling(value * ShockAbsorb)
        _Owner.addShock(shock)

        'check for bodypart destruction
        If DamageTaken >= Health Then Destroyed()
    End Sub
    Private Sub Destroyed()
        Console.WriteLine(Name & " has been destroyed!")
        _Owner.AddShock(ShockLoss)
        _Owner.Remove(Me)
    End Sub
#End Region
End Class
