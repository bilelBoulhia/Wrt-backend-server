FROM redis:latest

# Expose Redis port
EXPOSE 6379

# Copy redis.conf from the build context to the container's Redis config directory
COPY redis.conf /usr/local/etc/redis/redis.conf

# Run Redis with the custom config
CMD ["redis-server", "/usr/local/etc/redis/redis.conf"]
