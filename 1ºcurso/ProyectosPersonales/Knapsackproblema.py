

def Resolver(w,lista,I,memoria):
    r = I+1
    if I == len(lista) or w <= 0:
        return 0
    
    if lista[I][0] > w:
        return Resolver(w,lista,r,memoria)
    
    if memoria[I][w-1] != 0:
        return memoria[I][w-1]
    
    no_tomar = Resolver(w,lista,r,memoria)

    tomar = lista[I][1] + Resolver(w-lista[I][0],lista,r,memoria)
    
    memoria[I][w-1] = max(no_tomar,tomar)

    return memoria[I][w-1]
    
    



def main():
    CAPACIDAD = 200
    lista = [
    (3, 4), (4, 5), (7, 10), (8, 11), (9, 13),
    (2, 3), (1, 2), (6, 7), (5, 8), (4, 6),
    (3, 5), (7, 9), (2, 4), (6, 10), (5, 7),
    (9, 15), (8, 14), (3, 6), (4, 7), (2, 3),
    (1, 1), (10, 20), (11, 22), (12, 25), (13, 27),
    (4, 5), (5, 9), (6, 11), (7, 13), (8, 16),
    (3, 4), (2, 2), (9, 18), (10, 19), (11, 23),
    (12, 24), (13, 30), (14, 32), (15, 35), (16, 40),
    (5, 6), (6, 8), (7, 12), (8, 14), (9, 17),
    (10, 21), (11, 26), (12, 28), (13, 33), (14, 36)
]
    memoria = [[0] * CAPACIDAD for _ in lista]

    print(Resolver(CAPACIDAD,lista,0,memoria))

main()