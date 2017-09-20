# 3d Bin Packing
[![Build Status](https://img.shields.io/codeship/d6c1ddd0-16a3-0132-5f85-2e35c05e22b1/master.svg)]()
[![License](https://img.shields.io/hexpm/l/plug.svg)]()

<i>A freelance project for packing the goods efficiently using some heuristic algorithms </i>

<h4>3D BIN PACKING PROBLEM</h4>
Development of an Optimization software for calculation of the best container box for a given amount of items (boxes) with 2 possibilities:
<ul>
<li>
<p>BEST FIT: select the best container box(es) (less waste in unused volume) choosing it from an ordered list of possible prebuilt container boxes (with several constraints in the dimensions and max weight). The entire ordered list can be split into several containers (according MaxCount limit) when needed, even of different sizes.</p></li>

<li>OPTIMAL: calculate the dimensions of the ‘optimal’ container box(es) (with several constraints in the dimensions and max weight).The entire ordered list can be split into several containers (according MaxCount limit and the Min/Max dimensional constraints) when needed, even of different sizes.
The optimal calculated container box(es) is(are) almost cubic (if possible), have the maximum stability (low barycenter and no/minimum loose items inside), and a mimimal unused volume (air).</li>
<li>
Project Description can be viewed further <a href="https://github.com/SahibYar/3D-Bin-Packing/files/89321/3D.binning.problem.-.Project.Description.docx">here in the documentation</a>
</li>
</ul>
