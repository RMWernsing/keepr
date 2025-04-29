import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async addKeepToVault(vaultKeepData, keepId) {
    const response = await api.post('api/vaultkeeps', { vaultId: vaultKeepData, keepId: keepId })
    logger.log('here is your new vault keep!', response.data)
  }
}

export const vaultKeepsService = new VaultKeepsService()