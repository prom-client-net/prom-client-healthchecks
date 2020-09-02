#!/bin/bash

rm src/dummy
rm tests/dummy


proj_name=${PWD##*/} 
sed -i "s/prom-tmpl/${proj_name}/g" README.md

rm run-me.sh
