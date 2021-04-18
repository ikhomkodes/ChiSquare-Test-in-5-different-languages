# -*- coding: ikhomkodes -*-
"""
Created on Saturday, April 17, 2021

@author: Khom Raj Thapa Magar
#ikhomkodes
kyzen khom

ChiSquare Test is used to analyze the frequency table (i.e. contingency table), which is formed by two categorical
variables. The Chi-Square test evaluates whether there is a significant relationship between the categories of the
two variables.
"""

from scipy import stats
import numpy as np
import matplotlib.pyplot as plt

x = np.linspace(0, 10, 10)
fig, ax = plt.subplots(1, 1)

linestyles = [':', '--', '-.', '-']
deg_of_freedom = [1, 4, 7, 6]
for df, ls in zip(deg_of_freedom, linestyles):
    ax.plot(x, stats.chi2.pdf(x, df), linestyles=ls)

plt.xlim(0, 10)
plt.ylim(0, 0.4)

plt.xlabel('Value')
plt.ylabel('Frequency')
plt.title('Chi-Square Distribution')

plt.legend()
plt.show()


"""
@copyright (c) 2021 ikhomkodes
"""
