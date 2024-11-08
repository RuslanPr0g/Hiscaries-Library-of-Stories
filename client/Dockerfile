FROM node:21

RUN npm install -g @angular/cli

WORKDIR /
RUN mkdir angular-app
WORKDIR /angular-app

ENV APP_NAME 'my-app'
ENV ROUTING 'true'
ENV STANDALONE 'true'
ENV STRICT 'true'
ENV STYLE 'scss'

CMD ng new $APP_NAME --routing=$ROUTING --standalone=$STANDALONE --strict=$STRICT --style=$STYLE \
    && mv $APP_NAME/* . \
    && rm -rf $APP_NAME \
    && ng serve --host 0.0.0.0 --port 4001

EXPOSE 4001