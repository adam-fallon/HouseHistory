FROM php:8.2-rc-apache-bullseye
WORKDIR /var/www/site

# Copy the application code to the container
COPY www /var/www/site

# Install PHP extensions and other dependencies
RUN apt-get update && \
    apt-get install -y \
        libpng-dev \
        libjpeg-dev \
        libpq-dev \
        libzip-dev \
        zip \
        unzip && \
    docker-php-ext-configure gd --with-jpeg && \
    docker-php-ext-install -j$(nproc) \
        gd \
        pdo \
        pdo_mysql \
        pdo_pgsql \
        zip

# Enable Apache rewrite module
RUN a2enmod rewrite

# Update the default apache site with the config we created.
ADD apache-config.conf /etc/apache2/sites-enabled/000-default.conf

# Expose port 80 for the Apache server
EXPOSE 80

# Start the Apache server
CMD ["apache2-foreground"]