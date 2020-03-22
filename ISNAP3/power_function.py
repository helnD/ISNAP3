import numpy as np
import scipy.optimize as opt
import sys
import socket

def function(x, a, b, c):
    return a + b * x ** c


if __name__ == '__main__':
    params = sys.argv[1].split('|')
    
    x = np.array(list(map(float, params[0].split(' '))))
    y = np.array(list(map(float, params[1].split(' '))))

    true_params, _ = opt.curve_fit(function, x, y)

    str_parameters = ""

    for parameter in true_params:
        str_parameters += str(parameter) + ' '

    sock = socket.socket()
    sock.connect(('localhost', 5264))
    sock.send(str_parameters.encode('utf-8'))
    sock.close()

