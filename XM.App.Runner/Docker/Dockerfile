FROM nwndotnet/anvil:8193.37.0-dev.3.r3075e2d6
RUN wget https://aka.ms/getvsdbgsh -O - 2>/dev/null | /bin/sh /dev/stdin -v vs2019 -l /remote_debugger/vsdbg

RUN chmod 777 -R /remote_debugger/vsdbg
