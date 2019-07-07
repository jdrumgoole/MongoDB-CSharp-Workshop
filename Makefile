#
# Utilities for MongoDB C# workshop
#
#

MONGODBBINDIR="C:\Program Files\MongoDB\Server\4.0\bin"
MONGORESTORE=${MONGODBBINDIR}\\mongorestore

mugdata: download restore
	@echo "Installed MUG data"


download:
	curl -O  https://developer-advocacy-public.s3-eu-west-1.amazonaws.com/mongodb-workshop/MUGS_PRO_ONE.mdp.gz

restore:
	${MONGORESTORE} --drop --gzip --archive=MUGS_PRO_ONE.mdp.gz
