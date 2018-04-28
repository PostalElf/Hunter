Public Structure Attack
    Public Name As String
    Public Range As Integer
    Public Multiple As Integer
    Public Cooldown As Integer
    Public Accuracy As Integer
    Public Penetration As Integer

    Public DamageType As DamageType
    Public DamageFull As Integer
    Public DamageGlancing As Integer

    Public Sub New(ByVal entry As String)
        Dim ln As String() = entry.Split("|")
        For n = 0 To ln.Length - 1
            Dim c As Integer = CInt(ln(n))
            Select Case n
                Case 0 : Range = c
                Case 1 : Multiple = c
                Case 2 : Cooldown = c
                Case 3 : Accuracy = c
                Case 4 : Penetration = c
                Case 5 : DamageType = c
                Case 6 : DamageFull = c
                Case 7 : DamageGlancing = c
            End Select
        Next
    End Sub
    Public Sub New(ByVal _name As String, _
                   ByVal _range As Integer, ByVal _multiple As Integer, ByVal _cooldown As Integer, _
                   ByVal _accuracy As Integer, ByVal _penetration As Integer, _
                   ByVal _damageType As DamageType, ByVal _damageFull As Integer, ByVal _damageGlancing As Integer)
        Name = _name
        Range = _range
        Multiple = _multiple
        Cooldown = _cooldown
        Accuracy = _accuracy
        Penetration = _penetration
        DamageType = _damageType
        DamageFull = _damageFull
        DamageGlancing = _damageGlancing
    End Sub
    Public Function Export() As String
        Dim total As String = ""
        total &= Range & "|"
        total &= Multiple & "|"
        total &= Cooldown & "|"
        total &= Accuracy & "|"
        total &= Penetration & "|"
        total &= DamageType & "|"
        total &= DamageFull & "|"
        total &= DamageGlancing
        Return total
    End Function
    Public Overrides Function ToString() As String
        Dim total As String = Accuracy & "%"
        If Multiple > 1 Then total &= " x" & Multiple
        total &= " - " & DamageGlancing & "/" & DamageFull & " " & DamageType.ToString
        total &= " (" & Penetration & ")"
        Return total
    End Function
End Structure
