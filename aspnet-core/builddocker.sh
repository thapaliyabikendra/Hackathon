if [ -z "$1" ]
  then
    echo "***********************************************"
    echo ""
    echo "Version must be specified e.g. sh build.sh 1.0.0.0"
    echo ""
    echo "***********************************************"
    exit
fi

docker build . -f ./src/Drop.WMS.HttpApi.Host/Dockerfile -t drop-wms/identity-service:$1