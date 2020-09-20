# -*- coding: utf-8 -*-
"""
Created on Fri Sep 18 16:31:36 2020

@author: 29450
"""

from py2neo import Graph, Database 
from py2neo.data import Node, Relationship, Subgraph, walk
from py2neo.matching import *
from py2neo.ogm import *
import numpy as np
import re

class BearMachine():
    def __init__(self,graph):
        self.graph = graph
        self.init()
        pass
    
    def init(self):
        self.curr = None
        pass
    
    def hear_strings(self,pool):
        pool = self.__graph_generate_strings(pool)
        #self.__reconize_string(pool)
                
        pass
    
    def cut(self):
        self.curr = None
        pass
    
    def __graph_generate_strings(self,pool):
        result = []
        for string in pool:
            node = Node(color = string)
            self.graph.create(node)
            
            if self.curr is not None:
                rel = Relationship(self.curr,"t",node,r = "0", d = "1")
                self.graph.create(rel)
                result.append(rel)

                            
            result.append(node)
            self.curr = node
        
        
        
        pass
    
    
    pass

if __name__ == "__main__":
    graph =  Graph(password="Bb19980123")
    graph.delete_all()
    
    bm = BearMachine(graph)
    bm.hear_strings("我的名字是熊宣森。")
    bm.hear_strings("我喜欢吃三文鱼。")