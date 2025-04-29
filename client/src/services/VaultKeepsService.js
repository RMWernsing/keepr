import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"

class VaultKeepsService {
  async getVaultKeepById(vaultKeepId) {
    const response = await api.get(`api/vaultkeeps/${vaultKeepId}`)
    logger.log('here is your vault keep', response.data)
  }
  async deleteVaultKeep(vaultKeepId) {
    const response = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    // logger.log('you deleted a vault keep', response.data)
    const index = AppState.vaultKeeps.findIndex(keep => keep.vaultKeepId == vaultKeepId)
    AppState.vaultKeeps.splice(index, 1)

  }
  async addKeepToVault(vaultKeepData, keepId) {
    const response = await api.post('api/vaultkeeps', { vaultId: vaultKeepData, keepId: keepId })
    // logger.log('here is your new vault keep!', response.data)
  }
}

export const vaultKeepsService = new VaultKeepsService()