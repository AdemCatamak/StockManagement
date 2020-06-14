FROM rabbitmq:3.7-management

RUN apt-get update
RUN apt-get install -y curl unzip

# Supported Plugin Enabled
RUN rabbitmq-plugins enable --offline rabbitmq_shovel rabbitmq_shovel_management

# Download Delayed-Exchange-Plugin
RUN curl https://dl.bintray.com/rabbitmq/community-plugins/3.7.x/rabbitmq_delayed_message_exchange/rabbitmq_delayed_message_exchange-20171201-3.7.x.zip > rabbitmq_delayed_message_exchange-20171201-3.7.x.zip && \
unzip rabbitmq_delayed_message_exchange-20171201-3.7.x.zip && \
rm -f rabbitmq_delayed_message_exchange-20171201-3.7.x.zip && \
mv rabbitmq_delayed_message_exchange-20171201-3.7.x.ez plugins/

# Enabled Delayed-Exchange-Plugin
RUN rabbitmq-plugins enable rabbitmq_delayed_message_exchange