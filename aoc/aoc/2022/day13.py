from functools import cmp_to_key


def compare(l, r):
    res = 0
    for i in range(max(len(l), len(r))):
        if res != 0:
            return res
        if i >= len(l):
            return 1
        if i >= len(r):
            return -1

        x = l[i] 
        y = r[i] 
        #x and y is array
        if isinstance(x, list) and isinstance(y, list):
            res = compare(x, y)
        elif isinstance(x, list):
            res = compare(x, [y])
        elif isinstance(y, list):
            res = compare([x], y)
        elif x < y:
            return 1
        elif x > y:
            return -1
    return res

res = 0
with open('input/day13.in') as f:
    pairs = [x.split("\n") for x in f.read().split("\n\n")]
    packets = []
    for i,x in enumerate(pairs):
        packets.append(eval(x[0]))
        packets.append(eval(x[1]))
        if compare(eval(x[0]), eval(x[1])) == 1:
            res += i + 1

    print("part 1: ", res)

    packets.append(eval("[[2]]"))
    packets.append(eval("[[6]]"))

    packets.sort(key=cmp_to_key(compare) , reverse=True)
    a = packets.index(eval("[[2]]")) + 1
    b = packets.index(eval("[[6]]")) + 1

    print("part 2: ", a*b)
            


