const registry = new Map();

/**
 * Register a callback function with a specific ID
 */
export function registerCallback(id, cb) {
  registry.set(id, cb);
}

/**
 * Retrieve a callback by ID
 */
export function getCallback(id) {
    debugger;
  return registry.get(id);
}

/**
 * Remove a callback by ID
 */
export function removeCallback(id) {
  registry.delete(id);
}