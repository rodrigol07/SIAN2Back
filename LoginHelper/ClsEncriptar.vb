Public Class ClsEncriptar
    Dim CODE_KEY
    Dim QUERY_KEY, CODE_DECODE

    Function Encode(ByVal iValor As String)

        Dim eI, eMatAsc(), eMatHEX
        CODE_KEY = String.Empty
        ReDim Preserve eMatAsc(iValor.Length)
        For eI = 0 To Len(iValor) - 1

            eMatAsc(eI) = Asc(Mid(iValor, eI + 1, 1))
        Next
        eMatHEX = GetMatrizHex(eMatAsc)
        For eI = 0 To UBound(eMatHEX) - 1
            CODE_KEY = CODE_KEY + eMatHEX(eI)
        Next
        Encode = CODE_KEY

    End Function

    Function GetMatrizHex(ByVal Matriz As Object)

        Dim dJ As Integer
        Dim dMatHEX()
        ReDim Preserve dMatHEX(UBound(Matriz))
        For dJ = 0 To UBound(Matriz) - 1
            dMatHEX(dJ) = Hex(Matriz(dJ))
        Next
        GetMatrizHex = dMatHEX

    End Function

    Function Decode(ByVal iValor)

        Dim cI, cK, cNumDec(), cMatrizChar
        ReDim Preserve cNumDec(Len(iValor) / 2)
        cK = 0
        CODE_KEY = String.Empty
        For cI = Len(iValor) - 1 To 0 Step -2
            cNumDec(cK) = ConvertDec(Mid(iValor, cI, 2))
            cK = cK + 1
        Next
        cMatrizChar = GetMatrizChar(cNumDec)
        For cI = 0 To UBound(cMatrizChar) - 1
            CODE_KEY = cMatrizChar(cI) + CODE_KEY
        Next
        Decode = CODE_KEY

    End Function

    Function GetMatrizChar(ByVal Matriz As Object)

        Dim bJ
        Dim bMatChar()
        ReDim Preserve bMatChar(UBound(Matriz))
        For bJ = 0 To UBound(Matriz) - 1
            bMatChar(bJ) = Chr(Matriz(bJ))
        Next
        GetMatrizChar = bMatChar

    End Function

    Function ConvertDec(ByVal iValor)

        Dim aI, aSum, aNum, aK, aCad
        aSum = 0
        aK = 0
        For aI = Len(iValor) To 1 Step -1
            aCad = Mid(iValor, aI, 1)
            Select Case aCad
                Case "A" : aNum = 10
                Case "B" : aNum = 11
                Case "C" : aNum = 12
                Case "D" : aNum = 13
                Case "E" : aNum = 14
                Case "F" : aNum = 15
                Case Else
                    aNum = CInt(aCad)
            End Select
            aSum = aSum + aNum * (16 ^ aK)
            aK = aK + 1
        Next
        ConvertDec = aSum

    End Function
    Function request_querystring(ByVal qry, ByVal iKey)

        Dim xI, Found_It, fResultado, Found_It_Here, TrimExcess
        Found_It_Here = 0
        TrimExcess = 0
        fResultado = String.Empty
        If Len(qry) > 1 Then CODE_DECODE = Decode(qry)
        For xI = 0 To Len(iKey)
            Found_It = InStr(1, CODE_DECODE, "&" & iKey & "=", 1)
            If CInt(Found_It) > 0 Then
                Found_It_Here = Found_It
                TrimExcess = 1
            End If
            If Found_It_Here < 1 Then
                Found_It = InStr(1, CODE_DECODE, "?" & iKey & "=", 1)

                If (CInt(Found_It) > 0) Then
                    Found_It_Here = Found_It
                    TrimExcess = 2
                End If
            End If
            If Found_It_Here > 0 Then
                fResultado = Right(CODE_DECODE, (Len(CODE_DECODE)) - Found_It - Len(iKey) - TrimExcess)
                Found_It = InStr(1, fResultado, "&", 1)
                If CInt(Found_It) > 0 Then
                    fResultado = Left(fResultado, Found_It - 1)
                End If
            End If
        Next
        request_querystring = fResultado

    End Function

End Class
