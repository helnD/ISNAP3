﻿import numpy as np
import scipy.optimize as opt
import sys
import socket
import math
import time 

def exponential(x, a, b, c):
    return a + b * x ** c
    
def quadratic(x, a, b, c):
    return a + b * x + c * x ** 2
    
def trigonometric(x, a, b, c):
    return a + b * math.sin(x) + c * math.cos(x)
    
def get_function(name):
    if name == 'exponential':
        return exponential
    elif name == 'quadratic':
        return quadratic
    elif name == 'trigonometric':
        return trigonometric


if __name__ == '__main__':

    sock = socket.socket()
    sock.bind(('localhost', 5264))
    sock.listen(5)
    
    conn, addr = sock.accept()
    
    while True:
        
        try:
        
            message = conn.recv(1024).decode('utf-8').replace(',', '.')
            
        except ConnectionResetError:
            break
        
        params = message.split('|')
    
        functionType = params[0]
        x = np.array(list(map(float, params[1].split(' '))))
        y = np.array(list(map(float, params[2].split(' '))))

        true_params, _ = opt.curve_fit(get_function(functionType), x, y)
    
        str_parameters = ""
    
        for parameter in true_params:
            str_parameters += str(parameter) + ' '
    
        conn.send(str_parameters.encode('utf-8'))
    
    conn.close()