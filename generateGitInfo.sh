#!/bin/sh
echo "{ \"BuildInfo\": { \"GitHash\": \"$(git rev-parse HEAD)\", \"GitBranch\": \"$(git symbolic-ref --short HEAD)\", \"_BuildDateRaw\": $(date +%s) } }" > buildinfo.json
